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
    
    public partial class EnterGamePage : Window
    {
        public EnterGamePage()
        {
            InitializeComponent();
            SynchronizePlayerList();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.Disconnect();
            Application.Current.Shutdown();
        }

        public void SynchronizePlayerList()
        {
            PlayerListBox.Items.Clear();
            PlayerListBox.Items.Add(Client.INSTANCE.PlayerName);

            foreach (string playerName in Client.INSTANCE.OtherPlayer)
            {
                PlayerListBox.Items.Add(playerName);
            }
        }

        //todo
        private void EnterGame_Click(object sender, RoutedEventArgs e)
        {
           SetGameWindow setGameWindow = new SetGameWindow();
            setGameWindow.Show();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginWindow.shutDown(e);
        }
    }
}
