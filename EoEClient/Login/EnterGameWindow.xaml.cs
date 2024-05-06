using EoE.Network.Packets.GameEventPacket;
using System.Windows;


namespace EoE.Client.Login
{

    public partial class EnterGameWindow : Window
    {
        public bool ignoreClosing = false;
        public EnterGameWindow()
        {
            InitializeComponent();
            SynchronizeOtherPlayerList();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SynchronizeOtherPlayerList()
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

        private void EnterGame_Click(object sender, RoutedEventArgs e)
        {
            EnterGame.IsEnabled = false;
            EnterGame.Content = "Waiting...";
            Client.INSTANCE.SendPacket(new EnterGamePacket());
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
                    WindowManager.INSTANCE.GetWindows<SetGameWindow>().ignoreClosing = true;
                    WindowManager.INSTANCE.GetWindows<MainGameWindow>().ignoreClosing = true;

                    Client.INSTANCE.Disconnect();
                }
            }
        }
    }
}
