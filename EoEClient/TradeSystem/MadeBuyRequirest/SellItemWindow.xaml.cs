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

namespace WpfApp1.MadeBuyRequirest
{
    /// <summary>
    /// SellItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SellItemWindow : Window
    {
        private string selectedPlayer;
        private string tradeType;
        public SellItemWindow()
        {
            InitializeComponent();
        }
        public SellItemWindow(string playerName, string type)
        {

            selectedPlayer = playerName;
            tradeType = type;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            InputSellNumber InputSellNumberWindow = new InputSellNumber(selectedPlayer, tradeType);
            InputSellNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            BuyItemWindow buyItemWindow = new BuyItemWindow();
            buyItemWindow.Show();
            this.Close();
        }
    }
}
