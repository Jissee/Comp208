using EoE.Client.TradeSystem;
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
            addItem(1);
            addItem(2);
            addItem(3);
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
                itemShow.Text = tradeList.SelectedItem.ToString();
            }
                

            
        }

        public void addItem(int transcationnumber)
        {
            tradeList.Items.Add(transcationnumber);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (tradeList.SelectedItem != null)
            {
                tradeList.Items.Remove(tradeList.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select the transaction you want to delete.");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (tradeList.SelectedItem != null)

            {    MessageBox.Show("sucefully!");
                tradeList.Items.Remove(tradeList.SelectedItem);
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
    }
}
