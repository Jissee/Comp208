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
        bool ignoreClosing = false;
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
        public void SynchronizeGameSetting(int playerNumber, int gameRound)
        {
            player_number.Text = playerNumber.ToString();
            round_number.Text = gameRound.ToString();
        }
        //todo 不能之间进入游戏
        private void EnterGame_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MainGamePage>();
            ignoreClosing = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ignoreClosing)
            {
                MessageBoxResult result = MessageBox.Show("If you close this window, the program will stop running. Are you sure you want to close it?", "Close Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    Client.INSTANCE.Disconnect();
                    
                }
            }
        }
    }
}
