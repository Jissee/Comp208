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
    /// ChatMianPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChatMianPage : Window
    {
       
        public ChatMianPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.Hide();
                ChatPage chat = new ChatPage();
                 chat.Show();
            }
            else
            {
                MessageBox.Show("Please select a player.");
            }
            
            
        }
    }
}
