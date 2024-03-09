using EoE.Network.Packets;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EoE.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int X = 0;
        private Client clientAlice = new Client("Alice");
        private Client clientBob = new Client("Bob");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clientAlice.Connect("127.0.0.1", 25566);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clientAlice.Disconnect();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            clientBob.Disconnect();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            clientBob.Connect("127.0.0.1", 25566);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Alice send to bob
            RemotePlayer? plr = clientAlice.GetRemotePlayer("Bob");
            if(plr != null)
            {
                plr.SendPacket(new NewPacket(X++, 1.12));
            }
        }
    }
}