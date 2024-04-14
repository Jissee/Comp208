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

        public void RequireCreateOponTransaction(GameTransaction transaction)
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
                offeror.MsgBox("Successfully sent transaction request");
            }
            else
            {
                offeror.MsgBox("No enough Resources");
            }
        }
        public void RequireCreateSecretTransaction(GameTransaction transaction)
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
                    offeror.MsgBox("Successfully sent transaction request");
                }
                else
                {
                    offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
                }
            }
        }

        public void RequireCancelOpenTransaction(Guid id)
        {
            string operatorName = offeror.PlayerName;
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                { 
                    offeror.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Cancel,transaction));
                    offeror.MsgBox("Successfully sent cancellation request");
                }
                else
                {
                    offeror.MsgBox("No operation authority");
                }
            }
            else
            {
                offeror.MsgBox("The transaction has been cancelled or has been accepted by another player");
            }
        }
        public void RequireAcceptOpenTransaction(Guid id)
        {
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                offeror.MsgBox("The transaction has been cancelled or has been accepted by another player");
            }
            else
            {
                bool flag = true;
                foreach (var item in transaction.RecipientOffer)
                {
                    if (resources.GetResourceCount(item.Type) <item.Count)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    offeror.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Accept,transaction));
                    offeror.MsgBox("Successfully sent acceptance request");
                }
                else
                {
                    offeror.MsgBox("No enough reousrces");
                }
            }

        }

        public void RequireAlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer)
        {
            GameTransaction? transaction;
            string operatorName = offeror.PlayerName;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                offeror.MsgBox("The transaction has been cancelled or has been accepted by another player");
            }
            else if(transaction.Offeror != operatorName)
            {
                offeror.MsgBox("No operation authority");
            }
            else
            {
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.AddResourceStack(item);
                }

                bool flag = true;
                foreach (var item in offerorOffer)
                {
                    if (resources.GetResourceCount(item.Type) < item.Count)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < offerorOffer.Count; i++)
                    {
                        transaction.OfferorOffer[i] = offerorOffer[i];
                        transaction.RecipientOffer[i] = recipientOffer[i];
                    }
                    offeror.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Alter, transaction));
                    offeror.MsgBox("Successfully sent  modification request");
                }
                else
                {
                    offeror.MsgBox("No enough reousrces");
                }
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.AddResourceStack(item);
                }
            }
        }

        public void RequireAcceptSecretTransaction(GameTransaction transaction)
        {
            if (transaction.Recipient == null)
            {
                throw new Exception("Wrong call, not secrert Transaction");
            }
            bool flag = true;
            foreach (var item in transaction.RecipientOffer)
            {
                if (resources.GetResourceCount(item.Type)<item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                offeror.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Accept, transaction));
                offeror.MsgBox("Successfully sent acceptance request");
            }
            else
            {
                offeror.MsgBox("No enough reousrces");
            }

        }
        public void RejectSecretTransaction(GameTransaction transaction)
        {
            if (offeror.PlayerName == transaction.Recipient)
            {
                offeror.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Reject, transaction));
            }
        }
        public void CreateNewOpenTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call"); 
            }

            openOrders.Add(transaction);
        }
        public void RemoveOpenTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call");
            }

            openOrders.Remove(transaction);
        }
       
        public void Synchronize(List<GameTransaction> list)
        {
            openOrders = list;
        }
       
    }
}

