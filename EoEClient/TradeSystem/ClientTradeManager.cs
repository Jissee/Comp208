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
using System.Windows;
using EoE.Client.Login;

namespace EoE.Client.TradeSystem
{
    public class ClientTradeManager: IClientTradeManager
    {
        private List<GameTransaction> openOrders;
        private Dictionary<int, GameTransaction> transverter;
        public ClientTradeManager()
        {
            transverter = new Dictionary<int, GameTransaction>();
            openOrders =  new List<GameTransaction>();
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
                if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) < item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Create,transaction));
                MessageBox.Show("Successfully sent transaction request");
            }
            else
            {
                MessageBox.Show("No enough Resources");
            }
        }
        public void RequireCreateSecretTransaction(GameTransaction transaction)
        {
            if (transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatOpenTransaction instead");
            }
            if (Client.INSTANCE.OtherPlayer.FirstOrDefault(player => player == transaction.Recipient) == null)
            {
                MessageBox.Show("No such Recipient");
            }
            else
            {
                bool flag = true;
                foreach (var item in transaction.OfferorOffer)
                {
                    if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) < item.Count)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Creat,transaction));
                    MessageBox.Show("Successfully sent transaction request");
                }
                else
                {
                    MessageBox.Show("No enough Resources");
                }
            }
        }

        public void RequireCancelOpenTransaction(Guid id)
        {
            string operatorName = Client.INSTANCE.PlayerName;
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                if (operatorName == transaction.Offeror)
                {
                    Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Cancel,transaction));
                    MessageBox.Show("Successfully sent cancellation request");
                }
                else
                {
                    MessageBox.Show("No operation authority");
                }
            }
            else
            {
                MessageBox.Show("The transaction has been cancelled or has been accepted by another player");
            }
        }
        public void RequireAcceptOpenTransaction(Guid id)
        {
            GameTransaction? transaction;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                MessageBox.Show("The transaction has been cancelled or has been accepted by another player");
            }
            else
            {
                bool flag = true;
                foreach (var item in transaction.RecipientOffer)
                {
                    if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) <item.Count)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Accept,transaction));
                    MessageBox.Show("Successfully sent acceptance request");
                }
                else
                {
                    MessageBox.Show("No enough reousrces");
                }
            }

        }

        public void RequireAlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer)
        {
            GameTransaction? transaction;
            string operatorName = Client.INSTANCE.PlayerName;
            transaction = openOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                MessageBox.Show("The transaction has been cancelled or has been accepted by another player");
            }
            else if(transaction.Offeror != operatorName)
            {
                MessageBox.Show("No operation authority");
            }
            else
            {
                foreach (var item in transaction.OfferorOffer)
                {
                    Client.INSTANCE.GonveranceManager.ResourceList.AddResourceStack(item);
                }

                bool flag = true;
                foreach (var item in offerorOffer)
                {
                    if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) < item.Count)
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
                    Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Alter, transaction));
                    MessageBox.Show("Successfully sent  modification request");
                }
                else
                {
                    MessageBox.Show("No enough reousrces");
                }
                foreach (var item in transaction.OfferorOffer)
                {
                    Client.INSTANCE.GonveranceManager.ResourceList.AddResourceStack(item);
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
                if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type)<item.Count)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Accept, transaction));
                MessageBox.Show("Successfully sent acceptance request");
            }
            else
            {
                MessageBox.Show("No enough reousrces");
            }

        }
        public void RejectSecretTransaction(GameTransaction transaction)
        {
            if (Client.INSTANCE.PlayerName == transaction.Recipient)
            {
                Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Reject, transaction));
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
            int index = 1;
            openOrders = list;
            transverter.Clear();
            foreach (GameTransaction transaction in list)
            {
                transverter.Add(index, transaction);
                index++;
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                MainTradePage mainTrade = WindowManager.INSTANCE.GetWindows<MainTradePage>();
                mainTrade.SynchronizeTransaction(transverter);
            });
        }
       
    }
}

