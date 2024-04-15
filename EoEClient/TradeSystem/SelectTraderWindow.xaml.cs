using EoE.Client.Login;
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
    /// SelectTraderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTraderWindow : Window
    {
       
        public SelectTraderWindow()
        {
            InitializeComponent();
            
        }
  

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           EoE.Client.Login.MainTradePage mainTradePage = new EoE.Client.Login.MainTradePage();
           mainTradePage.Show();
            this.Close();

        }

        private void submit_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sucefully");
        }
    }
}
