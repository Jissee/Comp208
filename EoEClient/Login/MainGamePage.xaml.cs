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
using WarSystem;
using System.Windows.Navigation;
using GovernenceSystem;
using ChatSystem;


namespace EoE.Client
{
    
  
    public partial class MainGamePage : Window
    {
        private int roundCount = 0;
        public MainGamePage()
        {
            InitializeComponent();
            //contentFrame.Navigated += contentFrame_Navigated;

        }

        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            MainTradePage tradePage = new MainTradePage();
            tradePage.Show();
            this.Hide();
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
            WarMainPage mainPage = new WarMainPage();
            mainPage.Show();
        }
        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // 页面导航完成后执行的操作
        }

        private void Goverment_Click(object sender, RoutedEventArgs e)
        {
            GovernenceSystem.MainWindow mainWindow = new GovernenceSystem.MainWindow();
            mainWindow.Show();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
           ChatMianPage chatMianPage = new ChatMianPage();
            chatMianPage.Show();
        }
    }
}
