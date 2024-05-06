using EoE.Network.Packets.TreatyPacket;
using System.Windows;

namespace EoE.Client.WarSystem
{
    /// <summary>
    /// AbrogateTreaty.xaml 的交互逻辑
    /// </summary>
    public partial class AbrogateTreaty : Window
    {
        public AbrogateTreaty()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerList.SelectedItem != null)
            {
                string name = PlayerList.SelectedItem.ToString()!;
                BreakTreatyPacket packet = new BreakTreatyPacket(name);
                Client.INSTANCE.SendPacket(packet);
            }
        }
    }
}
