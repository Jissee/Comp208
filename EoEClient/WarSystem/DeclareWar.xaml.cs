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
    /// DeclareWar.xaml 的交互逻辑
    /// </summary>
    public partial class DeclareWar : Window
    {
        public DeclareWar()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WarMainPage window = WindowManager.INSTANCE.GetWindows<WarMainPage>();
            WarIntensionPacket packet = new WarIntensionPacket(listBox1.SelectedItem.ToString()!, WarMainPage.theWarName, [], []);
            Client.INSTANCE.SendPacket(packet);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null )
            {
                WarDeclarationPacket packet = new WarDeclarationPacket(WarMainPage.theWarName);
                Client.INSTANCE.SendPacket(packet);
                this.Hide();
                MessageBox.Show("You can manage your army in next page or find the page in Check status of War!");
                AllocateArmy allocateArmy = new AllocateArmy();
                allocateArmy .Show();
                MessageBox.Show(listBox1.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Please select an enemy from list.");
            }
        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WarInvitationPacket packet = new WarInvitationPacket(WarMainPage.theWarName, listBox2.SelectedItem.ToString()!);
            Client.INSTANCE.SendPacket(packet);
        }
    }
}
//no alliance with player is allowed, but a message box for notice is need?