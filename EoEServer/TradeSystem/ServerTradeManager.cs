using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.TradePacket;
using EoE.TradeSystem;


namespace EoE.Server.TradeSystem
{
    public class ServerTradeManager : IServerTradeManager
    {
        public List<GameTransaction> OpenOrders { get; private set; }
        private IServer server;
        public ServerTradeManager(IServer server)
        {
            this.server = server;
            OpenOrders = new List<GameTransaction>();
        }

        public void CreateOpenTransaction(GameTransaction transaction)
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
                OpenOrders.Add(transaction);
                offeror.SendPacket(new ServerMessagePacket("Transaction successfully created"));
                offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
                server.Boardcast(new OpenTransactionPacket(OpenTransactionOperation.Create, transaction), player => true);
            }
            else
            {
                offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
            }
        }
        public void CreateSecretTransaction(GameTransaction transaction)
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
            else if (server.GetPlayer(transaction.Recipient) == null)
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
                    offeror.SendPacket(new ServerMessagePacket("Transaction successfully created"));
                    offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
                    recipient.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Creat, transaction));
                }
                else
                {
                    offeror.SendPacket(new ServerMessagePacket("No enough Resources"));
                }
            }
        }

        public void CancelOpenTransaction(Guid id, string operatorName)
        {
            GameTransaction? transaction;
            transaction = OpenOrders.FirstOrDefault(t => t.Id == id);
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

                    server.Boardcast(new OpenTransactionPacket(OpenTransactionOperation.Cancel, transaction), player => true);
                    offeror.SendPacket(new ServerMessagePacket("Transaction successfully cancelled"));
                    offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
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
            transaction = OpenOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                recipient.SendPacket(new ServerMessagePacket("The transaction has been cancelled or has been accepted by another player"));
            }
            else
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                if (ExchangeResource(offeror, recipient, transaction))
                {
                    server.Boardcast(new OpenTransactionPacket(OpenTransactionOperation.Cancel, transaction), player => true);
                    offeror.SendPacket(new ServerMessagePacket("Your transaction has been accepted"));
                    recipient.SendPacket(new ServerMessagePacket("Trade successfully"));
                    offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
                    recipient.SendPacket(new ResourceUpdatePacket(recipient.GonveranceManager.ResourceList.GetResourceListRecord()));
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
            transaction = OpenOrders.FirstOrDefault(t => t.Id == id);
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

                    offeror.SendPacket(new ServerMessagePacket("Your transaction has been successfully altered"));
                    offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
                    server.Boardcast(new OpenTransactionPacket(OpenTransactionOperation.Alter, transaction), player => true);
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
                offeror.SendPacket(new ServerMessagePacket("Your private trade has been accepted"));
                recipient.SendPacket(new ResourceUpdatePacket(recipient.GonveranceManager.ResourceList.GetResourceListRecord()));
                offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
            }
        }

        public void RejectSecretTransaction(GameTransaction transaction, string operatorName)
        {
            if (operatorName == transaction.Recipient)
            {
                IPlayer offeror = server.GetPlayer(transaction.Offeror)!;
                IServerResourceList resources = offeror.GonveranceManager.ResourceList;
                foreach (var item in transaction.OfferorOffer)
                {
                    resources.AddResourceStack(item);
                }
                offeror.SendPacket(new ServerMessagePacket("Your private trade has been rejected"));
                offeror.SendPacket(new ResourceUpdatePacket(offeror.GonveranceManager.ResourceList.GetResourceListRecord()));
            }
        }


        private bool ExchangeResource(IPlayer offeror, IPlayer recipient, GameTransaction transaction)
        {
            IServerResourceList recipientResources = recipient.GonveranceManager.ResourceList;
            IServerResourceList offerorResources = offeror.GonveranceManager.ResourceList;
            bool flag = true;
            foreach (ResourceStack item in transaction.RecipientOffer)
            {
                if (recipientResources.GetResourceCount(item.Type) < item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                foreach (ResourceStack item in transaction.RecipientOffer)
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

        public void ClearAll(IPlayer offeror)
        {
            GameTransaction? transaction = OpenOrders.FirstOrDefault(t => t.Offeror == offeror.PlayerName);
            while (transaction != null)
            {
                OpenOrders.Remove(transaction);
                transaction = OpenOrders.FirstOrDefault(t => t.Offeror == offeror.PlayerName);
            }
            server.Boardcast(new OpenTransactionSynchronizePacket(OpenOrders.Count, OpenOrders), play => true);
        }
    }
}
