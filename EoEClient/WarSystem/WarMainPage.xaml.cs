using EoE.Network.Packets.TreatyPacket;
using EoE.Network.Packets.WarPacket;
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
    /// WarMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class WarMainPage : Window
    {
        public static string theWarName;
        

        public WarMainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            theWarName = warName.Text;
            WarDeclarablePacket packet = new WarDeclarablePacket(warName.Text, []);
            Client.INSTANCE.SendPacket(packet);
            WarNameQueryPacket warNamePacket = new WarNameQueryPacket([]);
            Client.INSTANCE.SendPacket(warNamePacket);
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<CheckStatus>();
            WarNameQueryRelatedPacket Packet = new WarNameQueryRelatedPacket([]);
            Client.INSTANCE.SendPacket(Packet);
        }

        private void buttonGoingWar_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<AllocateArmy>();
        }

       

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void warGoals_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<WarDetail>();
            WarDetail window = WindowManager.INSTANCE.GetWindows<WarDetail>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void AbrogateTreaty_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<AbrogateTreaty>();
            QueryTreatyPacket packet = new QueryTreatyPacket([]);
            Client.INSTANCE.SendPacket(packet);
        }
    }
}
