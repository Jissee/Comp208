using EoE.Client.TradeSystem;
using EoE.GovernanceSystem;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EoE.Client.Login
{


    public partial class MainTradeWindow : Window
    {

        public MainTradeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SelectTraderWindow>();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SellAndBuy2>();

        }

        

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tradeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tradeList.SelectedItem != null)
            {
                int number = int.Parse(tradeList.SelectedItem.ToString());
                GameTransaction transaction = Client.INSTANCE.TradeManager.GetGameTransaction(number);
                itemShow.Text = "Offeror: " + transaction.Offeror + "\n"
                    + "Offeror offer: " + transaction.OfferorOffer[0].ToString() + "\n"
                    + transaction.OfferorOffer[1].ToString() + "\n"
                    + transaction.OfferorOffer[2].ToString() + "\n"
                    + transaction.OfferorOffer[3].ToString() + "\n"
                    + transaction.OfferorOffer[4].ToString() + "\n"
                    + transaction.OfferorOffer[5].ToString() + "\n"
                    + "Recipient offer: " + transaction.RecipientOffer[0].ToString() + "\n"
                    + transaction.RecipientOffer[1].ToString() + "\n"
                    + transaction.RecipientOffer[2].ToString() + "\n"
                    + transaction.RecipientOffer[3].ToString() + "\n"
                    + transaction.RecipientOffer[4].ToString() + "\n"
                    + transaction.RecipientOffer[5].ToString() + "\n";

            }

        }
        public void SynchronizeTransaction(Dictionary<int, GameTransaction> transverter)
        {
            tradeList.Items.Clear();
            foreach (int key in transverter.Keys)
            {
                tradeList.Items.Add(key);
            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (tradeList.SelectedItem != null)
            {
                int transactionNumber = int.Parse(tradeList.SelectedItem.ToString());
                GameTransaction transaction = Client.INSTANCE.TradeManager.GetGameTransaction(transactionNumber);
                Client.INSTANCE.TradeManager.RequireCancelOpenTransaction(transaction);
            }
            else
            {
                MessageBox.Show("Please select the transaction you want to delete.");
            }
        }

        //Accept
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (tradeList.SelectedItem != null)
            {
                int transactionNumber = int.Parse(tradeList.SelectedItem.ToString());
                GameTransaction transaction = Client.INSTANCE.TradeManager.GetGameTransaction(transactionNumber);
                Client.INSTANCE.TradeManager.RequireAcceptOpenTransaction(transaction);
            }
            else
            {
                MessageBox.Show("Please select the transaction you want to acccept.");
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            silicon1.IsChecked = false;
            copper1.IsChecked = false;
            iron1.IsChecked = false;
            aluminum1.IsChecked = false;
            electronic1.IsChecked = false;
            industrial1.IsChecked = false;
            Client.INSTANCE.TradeManager.ShowTranscations(Client.INSTANCE.TradeManager.OpenOrders);

        }

        private void silicon1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Silicon);
        }

        private void copper1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Copper);
        }

        private void iron1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Iron);
        }

        private void auminum1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Aluminum);
        }

        private void electronic1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Electronic);
        }

        private void industrial1_Checked(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.TradeManager.ShowAndSelectTransaction(GameResourceType.Industrial);
        }
    }
}
