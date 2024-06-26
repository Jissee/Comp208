﻿using EoE.Client.Login;
using EoE.Governance;
using EoE.Network.Packets.TradePacket;
using EoE.Trade;
using System.Windows;

namespace EoE.Client.Trade
{
    public class ClientTradeManager : IClientTradeManager
    {
        public List<GameTransaction> OpenOrders { get; private set; }
        private Dictionary<int, GameTransaction> transverter;
        public ClientTradeManager()
        {
            transverter = new Dictionary<int, GameTransaction>();
            OpenOrders = new List<GameTransaction>();
        }

        public void ShowAndSelectTransaction(GameResourceType type)
        {
            List<GameTransaction> list = new List<GameTransaction>();
            foreach (GameTransaction transaction in OpenOrders)
            {
                foreach (ResourceStack stack in transaction.OfferorOffer)
                {
                    if (stack.Type == type && stack.Count != 0)
                    {
                        list.Add(transaction);
                    }
                }
            }
            ShowTranscations(list);
        }
        public GameTransaction GetGameTransaction(int transactionNumber)
        {
            if (transverter.ContainsKey(transactionNumber))
            {
                return transverter[transactionNumber];
            }
            else
            {
                throw new Exception("transactionNumber");
            }
        }
        public void CreateOpenTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call, use CreatSecretTransaction instead");
            }
            if (OpenOrders.Count >= ITradeManager.MAX_TRANSACTION_NUMBER)
            {
                MessageBox.Show("Too many trades, the exchange is crowded Please wait for existing trades to close");
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
                    Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Create, transaction));
                }
                else
                {
                    MessageBox.Show("No enough Resources");
                }
            }
        }
        public void CreateSecretTransaction(GameTransaction transaction)
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
                    Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Creat, transaction));
                }
                else
                {
                    MessageBox.Show("No enough Resources");
                }
            }
        }

        public void CancelOpenTransaction(GameTransaction transaction)
        {
            if (Client.INSTANCE.PlayerName == transaction.Offeror)
            {
                Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Cancel, transaction));
            }
            else
            {
                MessageBox.Show("No operation authority");
            }

        }
        public void AcceptOpenTransaction(GameTransaction transaction)
        {
            bool flag = true;
            foreach (var item in transaction.RecipientOffer)
            {
                if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) < item.Count)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                Client.INSTANCE.SendPacket(new OpenTransactionPacket(OpenTransactionOperation.Accept, transaction));
            }
            else
            {
                MessageBox.Show("No enough reousrces");
            }
        }

        public void AlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer)
        {
            GameTransaction? transaction;
            string operatorName = Client.INSTANCE.PlayerName;
            transaction = OpenOrders.FirstOrDefault(t => t.Id == id);
            if (transaction == null)
            {
                MessageBox.Show("The transaction has been cancelled or has been accepted by another player");
            }
            else if (transaction.Offeror != operatorName)
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

        public void AcceptSecretTransaction(GameTransaction transaction)
        {
            if (transaction.Recipient == null)
            {
                throw new Exception("Wrong call, not secrert Transaction");
            }
            bool flag = true;
            foreach (var item in transaction.RecipientOffer)
            {
                if (Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(item.Type) < item.Count)
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
                Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Reject, transaction));
                MessageBox.Show("No enough reousrces");
            }

        }
        public void RejectSecretTransaction(GameTransaction transaction)
        {
            if (transaction.Recipient == null)
            {
                throw new Exception("Wrong call, not secrert Transaction");
            }
            if (Client.INSTANCE.PlayerName == transaction.Recipient)
            {
                Client.INSTANCE.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Reject, transaction));
            }
        }

        public void AddOpenTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call");
            }

            OpenOrders.Add(transaction);
            Synchronize(OpenOrders);
        }
        public void RemoveOpenTransaction(GameTransaction transaction)
        {
            if (!transaction.IsOpen)
            {
                throw new Exception("wrong call");
            }
            OpenOrders.Remove(transaction);
            Synchronize(OpenOrders);
        }

        public void Synchronize(List<GameTransaction> list)
        {
            OpenOrders = list;
            ShowTranscations(list);

        }

        public void ShowTranscations(List<GameTransaction> list)
        {
            int index = 1;
            transverter.Clear();
            foreach (GameTransaction transaction in list)
            {
                transverter.Add(index, transaction);
                index++;
            }

            Application.Current.Dispatcher.Invoke((Delegate)(() =>
            {
                MainTradeWindow mainTrade = WindowManager.INSTANCE.GetWindows<MainTradeWindow>();
                mainTrade.SynchronizeTransaction(transverter);
            }));
        }

    }
}

