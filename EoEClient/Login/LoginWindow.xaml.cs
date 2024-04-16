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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EoE.Client.WarSystem;
using EoE.Network.Packets.GameEventPacket;

namespace EoE.Client.Login
{
    
    public partial class LoginWindow : Window
    {
        public bool ignoreClosing = false;
        public LoginWindow()
        {
            InitializeComponent();
            ServerAddress.Text = "127.0.0.1";
            portNumber.Text = "25566";
        }

        

        private void Quary_Click(object sender, RoutedEventArgs e)
        {
            GameAgreement gameAgreementWindow = new GameAgreement();
            gameAgreementWindow.Owner = this; 
            gameAgreementWindow.ShowDialog();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServerAddress.Text))
            {
                MessageBox.Show("Please enter the server address!");
                ServerAddress.Text = "127.0.0.1";
                return;
            }
            if (string.IsNullOrEmpty(portNumber.Text))
            {
                MessageBox.Show("Please enter the server port number!");
                portNumber.Text = "25566";
                return;
            }

            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                MessageBox.Show("Please enter the username!");
                return;
            }

            if (agreeCheckBox.IsChecked != true)
            {
                MessageBox.Show("Please agree to the game agreement first!");
                return;
            }

            Client.INSTANCE.SetPlayerName(Username.Text);
            Client.INSTANCE.Connect(ServerAddress.Text,int.Parse(portNumber.Text));
        }


            private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
            {
            
            }

        
        private void UpdateConnectButtonState()
        {
            bool isChecked = agreeCheckBox.IsChecked ?? false;

            if (isChecked)
            {
                bool isServerAddressValid = !string.IsNullOrWhiteSpace(ServerAddress.Text);
                bool isUsernameValid = !string.IsNullOrWhiteSpace(Username.Text);

                Connect.IsEnabled = isServerAddressValid && isUsernameValid;
            }
            else
            {
                Connect.IsEnabled = false;
            }
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
                    App.Current.Shutdown();
                }
            }

        }
        
    }
}
