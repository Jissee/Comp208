using EoE.GovernanceSystem;
using EoE.Server.GovernanceSystem;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = EoE.TradeSystem.Transaction;

namespace EoE.Server.TradeSystem
{
    public class TradeManager : ITradeManager
    {
        private List<Transaction> openOrders = new List<Transaction>();
        Server server;
        public TradeManager(Server server)
        {
            this.server = server;
        }

        public void CreatOponTransaction(Transaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatSecretTransaction instead");
            }
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
        public void CreatSecretTransaction(Transaction transaction)
        {
            if (transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatOpenTransaction instead");
            }
            ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
            if (offeror.GonveranceManager.ResourceList.GetResourceCount(transaction.OfferorOffer.Type) >= transaction.OfferorOffer.Count)
            {
                _ = offeror.GonveranceManager.ResourceList.SplitResourceStack(transaction.OfferorOffer);
                //Todo Send packet to recipientName
            }
            else
            {
                throw new Exception();
                //Todo no enough resources
            }

        }

        public void CancelOpenTransaction(Guid id,string operatorName)
        {
            Transaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                {
                    ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
                    offeror.GonveranceManager.ResourceList.AddResource(transaction.OfferorOffer);
                    openOrders.Remove(transaction);
                    SynchronousOpenTrading(transaction);
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
            Transaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                throw new Exception();
                //Todo transaction already accepted or canceled
            }
            else
            {
                ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
                ExchangeResource(offeror, recipient, transaction);
                SynchronousOpenTrading(transaction);
            }
            
        }

        public void AlterOpenTransaction(Guid id, ResourceStack offerorOffer, ResourceStack recipientOffer)
        {
            Transaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                throw new Exception();
                //Todo transaction already accepted or canceled
            }
            else
            {
                ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
                ServerPlayerResourceList resourceList = offeror.GonveranceManager.ResourceList;

                if (offerorOffer.Type == transaction.OfferorOffer.Type)
                {
                    if (transaction.OfferorOffer.Count > offerorOffer.Count)
                    {
                        _ = transaction.OfferorOffer.Split(offerorOffer.Count);
                        resourceList.AddResource(transaction.OfferorOffer);
                        transaction.OfferorOffer = offerorOffer;
                    }
                    else if (transaction.OfferorOffer.Count < offerorOffer.Count)
                    {
                        int margin = offerorOffer.Count - transaction.OfferorOffer.Count;
                        if (resourceList.GetResourceCount(offerorOffer.Type) >= margin)
                        {
                            _ = resourceList.SplitResourceStack(offerorOffer.Type, margin);
                            transaction.OfferorOffer = offerorOffer;
                        }
                        else
                        {
                            throw new Exception("no enough resources");
                            //TODO
                        }
                    }
                }
                else
                {
                    if (resourceList.GetResourceCount(offerorOffer.Type) >= offerorOffer.Count)
                    {
                        resourceList.AddResource(transaction.OfferorOffer);
                        resourceList.SplitResourceStack(offerorOffer);
                        transaction.OfferorOffer = offerorOffer;
                    }
                    else
                    {
                        throw new Exception("no enough resources");
                        //TODO
                    }
                }

                transaction.RecipientOffer = recipientOffer;
                SynchronousOpenTrading(transaction);
            }
        }

        public void AcceptSecretTransaction(Transaction transaction)
        {
            if (transaction.Recipient == null)
            {
                throw new Exception("Wrong call, not secrert Transaction");
            }

            ServerPlayer recipient = (ServerPlayer)server.GetPlayer(transaction.Recipient)!;
            ServerPlayer offeror = (ServerPlayer)server.GetPlayer(transaction.Offeror)!;
            ExchangeResource(offeror, recipient, transaction);
            // Todo send Packet
        }

        private void SynchronousOpenTrading(Transaction transaction)
        {
            if (transaction.IsOpen)
            {
                //Todo broadcast transaction

            }
         
        }

        private void ExchangeResource(ServerPlayer offeror, ServerPlayer recipient, Transaction transaction)
        {
            if (recipient.GonveranceManager.ResourceList.GetResourceCount(transaction.RecipientOffer.Type) >= transaction.RecipientOffer.Count)
            {
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
}
