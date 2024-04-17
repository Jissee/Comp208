using EoE.Client.ChatSystem;
using EoE.Network.Packets.TreatyPacket;
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
            if(PlayerList.SelectedItem != null)
            {
                string name = PlayerList.SelectedItem.ToString()!;
                BreakTreatyPacket packet = new BreakTreatyPacket(name);
                Client.INSTANCE.SendPacket(packet);
            }
        }
    }
}
