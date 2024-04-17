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

namespace EoE.Client.ChatSystem
{
    /// <summary>
    /// chat.xaml 的交互逻辑
    /// </summary>
    public partial class ChatPage : Window
    {

        public ChatPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<EstablishRelation>();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SelectTraderWindow>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
