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
using EoE.GovernanceSystem;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket.Record;


namespace EoE.Client.Login
{
    public partial class MainGamePage : Window
    {
        private int roundCount = 0;
        public MainGamePage()
        {
            InitializeComponent();
            Population.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();
            Silicon.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Silicon).ToString();
            Copper.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Copper).ToString();
            Aluminum.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Aluminum).ToString();
            Iron.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Iron).ToString();
            Electronic.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Electronic).ToString();
            Industrial.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Industrial).ToString();

        }

        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MainTradePage>();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Client.INSTANCE.SendPacket(new FinishTickPacket(true));
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
           WindowManager.INSTANCE.ShowWindows<ChatMianPage>();
        }

        public void SynchronizeResources(ResourceListRecord resourceListRecord)
        {
            Silicon.Text = resourceListRecord.siliconCount.ToString();
            Copper.Text = resourceListRecord.copperCount.ToString();
            Aluminum.Text = resourceListRecord.aluminumCount.ToString();
            Iron.Text = resourceListRecord.ironCount.ToString();
            Electronic.Text = resourceListRecord.electronicCount.ToString();
            Industrial.Text = resourceListRecord.industrialCount.ToString();
        }
        public void SynchronizePopulation(PopulationRecord populationRecord)
        {
            Population.Text = populationRecord.availablePopulation.ToString();
        }
        public void SynchronizeRoundNumber(int round)
        {
            RoundCount.Text = round.ToString();
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (NextRound.IsChecked == true)
            {
                Client.INSTANCE.SendPacket(new FinishTickPacket(true));
            }
            else
            {
                Client.INSTANCE.SendPacket(new FinishTickPacket(false));
            }
        }
    }
}
