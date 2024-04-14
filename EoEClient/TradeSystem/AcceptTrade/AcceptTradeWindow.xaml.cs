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

namespace EoE.Client.TradeSystem
{
    /// <summary>
    /// AcceptTradeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AcceptTradeWindow : Window
    {
        public AcceptTradeWindow()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            MainTradePage mainTradePage = new MainTradePage();
            mainTradePage.Show();
            this.Close();
        }

        private void reject_Click(object sender, RoutedEventArgs e)
        {
            MainTradePage mainTradePage = new MainTradePage();
            mainTradePage.Show();
            this.Close();
        }
    }
}
