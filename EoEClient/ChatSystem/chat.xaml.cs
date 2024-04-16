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

namespace ChatSystem
{
    /// <summary>
    /// chat.xaml 的交互逻辑
    /// </summary>
    public partial class chat : Window
    {

        public chat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EstablishRelation establishRelation = new EstablishRelation();
            establishRelation.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectTraderWindow selectTraderWindow = new SelectTraderWindow();
            selectTraderWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
