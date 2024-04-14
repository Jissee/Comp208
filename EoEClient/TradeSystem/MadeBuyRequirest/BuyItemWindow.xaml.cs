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

namespace WpfApp1.MadeBuyRequirest
{
    /// <summary>
    /// BuyItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BuyItemWindow : Window
    {
        private string selectedPlayer;
        private string tradeType;

        public BuyItemWindow()
        {
            InitializeComponent();
        }
        public BuyItemWindow(string playerName, string type)
        {

            selectedPlayer = playerName;
            tradeType = type;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            InputBuyNumber InputBuyNumberWindow = new InputBuyNumber(selectedPlayer, tradeType);
            InputBuyNumberWindow.Show();
            this.Hide();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MainTradePage mainTradePage = new MainTradePage();
            mainTradePage.Show();
            this.Close();
        }
    }
}
