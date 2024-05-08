using EoE.Client.Chat;
using EoE.Client.Governance;
using EoE.Client.War;
using EoE.Governance;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket.Record;
using System.Windows;


namespace EoE.Client.Login
{
    public partial class MainGameWindow : Window
    {
        public bool ignoreClosing = false;
        public MainGameWindow()
        {
            InitializeComponent();

            PlayerName.Text = Client.INSTANCE.PlayerName;
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
            WindowManager.INSTANCE.ShowWindows<MainTradeWindow>();
        }

        private void War_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<WarMainPage>();
        }

        private void Goverment_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<GovernenceMainWindow>();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<ChatWindow>();
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

        public void SynchronizeOtherPlayerList()
        {
            PlayerName.Text = Client.INSTANCE.PlayerName;
        }
        public void SynchronizeRoundNumber(int round)
        {
            RoundCount.Text = round.ToString();
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
                    WindowManager.INSTANCE.GetWindows<SetGameWindow>().ignoreClosing = true;
                    Client.INSTANCE.Disconnect();
                }
            }

        }

        private void CheckBox_Clicked(object sender, RoutedEventArgs e)
        {
            if (NextRound.IsChecked == true)
            {
                Client.INSTANCE.SendPacket(new FinishTickPacket(true, int.Parse(RoundCount.Text)));
            }
            else
            {
                Client.INSTANCE.SendPacket(new FinishTickPacket(true, int.Parse(RoundCount.Text)));
            }
        }
    }
}
