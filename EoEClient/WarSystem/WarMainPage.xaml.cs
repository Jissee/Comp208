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
        private static WarMainPage instance;
        public static WarMainPage INSTANCE
        {
            get
            {
                if (instance == null || !instance.IsLoaded)
                {
                    instance = new WarMainPage();
                }
                return instance;
            }
        }

        public WarMainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeclareWar declareWar = new DeclareWar();
            declareWar.Show();
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<CheckStatus>();
            WarNameQueryPacket Packet = new WarNameQueryPacket([]);
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }

        private void AbrogateTreaty_Click(object sender, RoutedEventArgs e)
        {
            AbrogateTreaty.INSTANCE.Show();
        }
    }
}
