using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EoE.Server.TradeSystem
{
    public class TradeManager
    {
        private List<Transaction> openOrders = new List<Transaction>();
        private List<Transaction> secretOrders = new List<Transaction>();
        Server server;
        public TradeManager(Server server)
        {
            this.server = server;
        }

        public void CreatOponTransaction(Transaction transaction)
        {
            ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
            if (offeror.GonveranceManager.ResourceList.GetResourceCount(transaction.OfferorOffer.Type) >= transaction.OfferorOffer.Count)
            {
               _= offeror.GonveranceManager.ResourceList.SplitResourceStack(transaction.OfferorOffer);
                openOrders.Add(transaction);
                //Todo send OponTransaction packet
            }
            else
            {
                throw new Exception();
                //Todo no enough resources
            }
        }

        public void CancelTransaction(Guid id,string operatorName)
        {
            var transaction = openOrders.FirstOrDefault(t => t.Id == id);

            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                {
                    ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
                    offeror.GonveranceManager.ResourceList.AddResource(transaction.OfferorOffer);
                    openOrders.Remove(transaction);
                }
                else
                {
                    throw new Exception();
                    //Todo: no operation authority
                }

            }  
        }

        public void AcceptOpenTransaction(Guid id, string recipientName)
        {
            ServerPlayer recipient = (ServerPlayer)server.GetPlayer(recipientName)!;
            var transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                throw new Exception();
                //Todo transaction already accepted or canceled
            }
            else
            {
                if (recipient.GonveranceManager.ResourceList.GetResourceCount(transaction.RecipientOffer.Type) >= transaction.RecipientOffer.Count)
                {
                    ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
                    ResourceStack resource = recipient.GonveranceManager.ResourceList.SplitResourceStack(transaction.RecipientOffer);
                    offeror.GonveranceManager.ResourceList.AddResource(resource);
                    recipient.GonveranceManager.ResourceList.AddResource(transaction.OfferorOffer);
                    openOrders.Remove(transaction);
                }
                else
                {
                    throw new Exception();
                    //Todo recipient doesn't have enough resources
                }

            }
            
        }

        public void AcceptSecretTransaction(Guid id, string recipientName)
        {

        }

        private void ExchangeResource(ServerPlayer offeror, ServerPlayer recipient, Transaction transaction)
        {

        }

        public void CreatSecretTransaction(Transaction transaction, string recipientName)
        {
            ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
            if (offeror.GonveranceManager.ResourceList.GetResourceCount(transaction.OfferorOffer.Type)>= transaction.OfferorOffer.Count)
            {
                _ = offeror.GonveranceManager.ResourceList.SplitResourceStack(transaction.OfferorOffer);
                secretOrders.Add(transaction);
                //Todo Send packet to recipientName
            }
            else
            {
                throw new Exception();
                //Todo no enough resources
            }

        }
    }
}
