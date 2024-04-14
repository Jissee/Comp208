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

namespace WpfApp1.Login.TradeSystem.AcceptTrade
{
    /// <summary>
    /// RequirestList.xaml 的交互逻辑
    /// </summary>
    public partial class RequirestList : Window
    {
        public RequirestList()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                string str = (String)((ListBoxItem)listbox.SelectedItem).Content;
                // 现在您可以使用 str 变量进行后续操作
            }
            
            if (listbox.SelectedItem != null)
            {
                AcceptTradeWindow acceptTradeWindow = new AcceptTradeWindow();
                acceptTradeWindow.Show();
                this.Hide();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainTradePage mainTradePage = new MainTradePage();
            mainTradePage.Show();
            this.Hide();
        }
    }
}
