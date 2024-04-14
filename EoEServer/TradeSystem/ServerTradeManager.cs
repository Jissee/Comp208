using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.TradePacket;
using EoE.Server.GovernanceSystem;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EoE.Server.TradeSystem
{
    public class ServerTradeManager : ITradeManager
    {
        private List<GameTransaction> openOrders = new List<GameTransaction>();
        private IServer server;
        public ServerTradeManager(IServer server)
        {
            this.server = server;
        }

        public void CreatOponTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatSecretTransaction instead");
            }
            IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
            IServerResourceList resources = offeror.GonveranceManager.ResourceList;
            bool flag = true;
            foreach (var item in transaction.OfferorOffer)
            {
                if (resources.GetResourceCount(item.Type)< item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.SplitResourceStack(item);
                }
                openOrders.Add(transaction);
                server.Broadcast(new OpenTransactionPacket(OpenTransactionOperation.Create, transaction), player =>true);
            }
            else
            {
                offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
            }
        }
        public void CreatSecretTransaction(GameTransaction transaction)
        {
            if (transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatOpenTransaction instead");
            }
            if (transaction.Recipient == null)
            {
                server.GetPlayer(transaction.Offeror)!.SendPacket(new ServerMessagePacket("The player does not exist"));
                return;
            }
            else if(server.GetPlayer(transaction.Recipient) == null)
            {
                server.GetPlayer(transaction.Offeror)!.SendPacket(new ServerMessagePacket("The player does not exist"));
                return;
            }
            else
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                IServerResourceList resources = offeror.GonveranceManager.ResourceList;
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
                    foreach (var item in transaction.OfferorOffer)
                    {
                        resources.SplitResourceStack(item);
                    }
                    IPlayer recipient = server.GetPlayer(transaction.Recipient)!;
                    recipient.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Creat, transaction));
                }
                else
                {
                    offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
                }
            }
        }

        public void CancelOpenTransaction(Guid id,string operatorName)
        {
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                {
                    IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                    IServerResourceList resources = offeror.GonveranceManager.ResourceList;

                    foreach (var item in transaction.OfferorOffer)
                    {
                        resources.AddResourceStack(item);
                    }

                    server.Broadcast(new OpenTransactionPacket(OpenTransactionOperation.Cancel, transaction),player=> true);
                    offeror.SendPacket(new ResourceUpdatePacket(new ResourceListRecord (offeror.GonveranceManager.ResourceList)));
                }
                else
                {
                    IPlayer manipulator = server.GetPlayer(operatorName)!;
                    manipulator.SendPacket(new ServerMessagePacket("No operation authority"));
                }
            }
            else
            {
                IPlayer player = server.GetPlayer(operatorName)!;
                player.SendPacket(new ServerMessagePacket("The transaction has been cancelled or has been accepted by another player"));
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
                if(ExchangeResource(offeror, recipient, transaction))
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
                IServerResourceList resourceList = offeror.GonveranceManager.ResourceList;
                foreach (var item in transaction.OfferorOffer)
                {
                    resourceList.AddResourceStack(item);
                }

                bool flag = true;
                foreach (var item in offerorOffer)
                {
                    if (resourceList.GetResourceCount(item.Type)< item.Count)
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
                    server.Broadcast(new OpenTransactionPacket(OpenTransactionOperation.Alter,transaction),player => true);
                }
                else
                {
                    foreach (var item in transaction.OfferorOffer)
                    {
                        resourceList.SplitResourceStack(item);
                    }

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
                recipient.SendPacket(new ResourceUpdatePacket(new ResourceListRecord (recipient.GonveranceManager.ResourceList)));
                offeror.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(recipient.GonveranceManager.ResourceList)));
            }
        }

        public void RejectSecretTransaction(GameTransaction transaction,string operatorName)
        {
            if (operatorName == transaction.Recipient)
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                IServerResourceList resources = offeror.GonveranceManager.ResourceList;
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.AddResourceStack(item);
                }
                offeror.SendPacket(new ServerMessagePacket("Counterparty rejects transaction"));
            }
        }


        private bool ExchangeResource(IPlayer offeror, IPlayer recipient, GameTransaction transaction)
        {
            IServerResourceList recipientResources = recipient.GonveranceManager.ResourceList;
            IServerResourceList offerorResources = offeror.GonveranceManager.ResourceList;
            bool flag = true;
            foreach (var item in transaction.RecipientOffer)
            {
                if (recipientResources.GetResourceCount(item.Type)< item.Count)
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
