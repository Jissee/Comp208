using EoE.GovernanceSystem.ServerInterface;
using EoE.GovernanceSystem;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.TradePacket;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.ClientInterface;

namespace EoE.Client.TradeSystem
{
    public class ClientTradeManager
    {
        private List<GameTransaction> openOrders = new List<GameTransaction>();
        IClient offeror = Client.INSTANCE;
        IClientResourceList resources = Client.INSTANCE.GonveranceManager.ResourceList;
        public ClientTradeManager()
        {
           
        }

        public void ApplyOponTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatSecretTransaction instead");
            }
            bool flag = true;
            foreach (var item in transaction.OfferorOffer)
            {
                if (resources.GetResourceCount(item.Type) < item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                offeror.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Create,transaction));
            }
            else
            {
                offeror.MsgBox("No enough Resources");
            }
        }
        public void ApplpySecretTransaction(GameTransaction transaction)
        {
            if (transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatOpenTransaction instead");
            }
            if (offeror.OtherPlayer.FirstOrDefault(player => player == transaction.Recipient) == null)
            {
                offeror.MsgBox("No such Recipient");
            }
            else
            {
                bool flag = true;
                foreach (var item in transaction.OfferorOffer)
                {
                    if (resources.GetResourceCount(item.Type) < item.Count)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    offeror.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Creat,transaction));
                }
                else
                {
                    offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
                }
            }
        }

        public void ApplyCancelOpenTransaction(Guid id)
        {
            string operatorName = offeror.PlayerName;
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                { 
                    offeror.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Cancel,transaction));
                }
                else
                {
                    offeror.MsgBox("No operation authority");
                }
            }
        }
        public void AcceptOpenTransaction(Guid id, string recipientName)
        {
            IPlayer recipient = server.GetPlayer(recipientName)!;
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                recipient.SendPacket(new ServerMessagePacket("The transaction has been cancelled or has been accepted by another player"));
            }
            else
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                if (ExchangeResource(offeror, recipient, transaction))
                {
                    server.Broadcast(new OpenTransactionPacket(OpenTransactionOperation.Cancel, transaction), player => true);
                    offeror.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(offeror.GonveranceManager.ResourceList)));
                    recipient.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(recipient.GonveranceManager.ResourceList)));
                }
                else
                {
                    recipient.SendPacket(new ServerMessagePacket("No enough resources"));
                }

            }

        }

        public void AlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer, string operatorName)
        {
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            IPlayer manipulator = server.GetPlayer(operatorName)!;
            if (transaction == null)
            {
                manipulator.SendPacket(new ServerMessagePacket("The transaction has been cancelled or has been accepted by another player"));
            }
            else
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                IClientResourceList resourceList = offeror.GonveranceManager.ResourceList;
                foreach (var item in transaction.OfferorOffer)
                {
                    resourceList.AddResourceStack(item);
                }

                bool flag = true;
                foreach (var item in offerorOffer)
                {
                    if (resourceList.GetResourceCount(item.Type) < item.Count)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < offerorOffer.Count; i++)
                    {
                        resourceList.SplitResourceStack(offerorOffer[i]);
                        transaction.OfferorOffer[i] = offerorOffer[i];
                    }

                    for (int i = 0; i < recipientOffer.Count; i++)
                    {
                        transaction.RecipientOffer[i] = recipientOffer[i];
                    }
                    offeror.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(offeror.GonveranceManager.ResourceList)));
                    server.Broadcast(new OpenTransactionPacket(OpenTransactionOperation.Alter, transaction), player => true);
                }

            }
        }

        public void AcceptSecretTransaction(GameTransaction transaction)
        {
            if (transaction.Recipient == null)
            {
                throw new Exception("Wrong call, not secrert Transaction");
            }

            IPlayer recipient = server.GetPlayer(transaction.Recipient)!;
            IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
            if (!ExchangeResource(offeror, recipient, transaction))
            {
                recipient.SendPacket(new ServerMessagePacket("No enough resources"));
                RejectSecretTransaction(transaction, transaction.Recipient);
            }
            else
            {
                recipient.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(recipient.GonveranceManager.ResourceList)));
                offeror.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(recipient.GonveranceManager.ResourceList)));
            }
        }

        public void RejectSecretTransaction(GameTransaction transaction, string operatorName)
        {
            if (operatorName == transaction.Recipient)
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                IClientResourceList resources = offeror.GonveranceManager.ResourceList;
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.AddResourceStack(item);
                }
                offeror.SendPacket(new ServerMessagePacket("Counterparty rejects transaction"));
            }
        }


        private bool ExchangeResource(IPlayer offeror, IPlayer recipient, GameTransaction transaction)
        {
            IClientResourceList recipientResources = recipient.GonveranceManager.ResourceList;
            IClientResourceList offerorResources = offeror.GonveranceManager.ResourceList;
            bool flag = true;
            foreach (var item in transaction.RecipientOffer)
            {
                if (recipientResources.GetResourceCount(item.Type) < item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                foreach (var item in transaction.RecipientOffer)
                {
                    recipientResources.SplitResourceStack(item);
                    offerorResources.AddResourceStack(item);
                }

                foreach (var item in transaction.OfferorOffer)
                {
                    recipientResources.AddResourceStack(item);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

