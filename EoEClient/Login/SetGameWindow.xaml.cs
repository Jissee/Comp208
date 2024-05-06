using EoE.Network.Packets.GameEventPacket;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EoE.Client.Login
{

    public partial class SetGameWindow : Window
    {
        bool amount = false;
        bool round = false;
        public bool ignoreClosing = false;
        public SetGameWindow()
        {
            InitializeComponent();
        }


        private void Confirm_Click_1(object sender, RoutedEventArgs e)
        {
            if (amount && round)
            {
                int playerCount;
                int roundCount;
                if (int.TryParse(SetResource.Text, out playerCount) && int.TryParse(selectedValueTextBox.Text, out roundCount))
                {
                    Client.INSTANCE.SendPacket(new GameSettingPacket(new GameSettingRecord(playerCount, roundCount)));
                    this.Hide();
                    WindowManager.INSTANCE.ShowWindows<EnterGameWindow>();
                }
            }
            else
            {
                MessageBox.Show("You haven't changed any values. If you confirm, please submit again.");
                amount = true;
                round = true;
            }

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            round = true;
            if (selectedValueTextBox != null)
            {
                selectedValueTextBox.Text = ((int)e.NewValue).ToString();
            }
        }

        private void SetResource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            amount = true;
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
                    WindowManager.INSTANCE.GetWindows<EnterGameWindow>().ignoreClosing = true;
                    WindowManager.INSTANCE.GetWindows<MainGameWindow>().ignoreClosing = true;
                    Client.INSTANCE.Disconnect();
                }
            }

        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
