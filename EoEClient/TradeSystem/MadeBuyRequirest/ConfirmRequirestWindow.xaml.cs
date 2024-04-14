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
    /// ConfirmRequirestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmRequirestWindow : Window
    {
        public ConfirmRequirestWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainTradePage mainWindow = new MainTradePage();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SellItemWindow sellItemWindow = new SellItemWindow();
            sellItemWindow.Show();
            this.Close();
        }
    }
}
