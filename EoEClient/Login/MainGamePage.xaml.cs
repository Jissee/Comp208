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
using EoE.Client.WarSystem;
using System.Windows.Navigation;
using EoE.Client.GovernanceSystem;
using EoE.Client.ChatSystem;


namespace EoE.Client.Login
{
    public partial class MainGamePage : Window
    {
        private int roundCount = 0;
        public MainGamePage()
        {
            InitializeComponent();

        }

        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            WindowsManager.INSTANCE.ShowWindows<MainTradePage>();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            roundCount++; 
            UpdateRoundCountText(); 
        }

        //记录游戏回合数
        private void UpdateRoundCountText()
        {
            RoundCount.Text = roundCount.ToString(); 
        }

        private void War_Click(object sender, RoutedEventArgs e)
        {
            WarMainPage.INSTANCE.Show();
        }
        
        private void Goverment_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.INSTANCE.Show();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
           WindowsManager.INSTANCE.ShowWindows<ChatMianPage>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
