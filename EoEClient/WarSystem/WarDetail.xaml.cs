using EoE.Network.Packets.WarPacket;
using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// WarDetail.xaml 的交互逻辑
    /// </summary>
    public partial class WarDetail : Window
    {
        public WarDetail()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WarTarget target = new WarTarget();
            target.SiliconClaim = int.Parse(Silicon.Text);
            target.IronClaim = int.Parse(Iron.Text);
            target.CopperClaim = int.Parse(Copper.Text);
            target.AluminumClaim = int.Parse(Aluminum.Text);
            target.ElectronicClaim = int.Parse(Electronic.Text);
            target.IndustrialClaim = int.Parse(Industrial.Text);
            target.PopClaim = int.Parse(Population.Text);
            target.FieldClaim = int.Parse(Blocks.Text);

            WarTargetPacket packet = new WarTargetPacket(OtherPlayerName.SelectedItem.ToString()!, target); ;
            MessageBox.Show("You have successfully submitted.");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = OtherPlayerName.SelectedItem.ToString()!;
            if (((ClientWarTargetList)Client.INSTANCE.ClientWarTargetList).WarTargetList.ContainsKey(name))
            {
                WarQueryTargetPacket packet = new WarQueryTargetPacket(name,new WarTarget());
                Client.INSTANCE.SendPacket(packet);
            }
            else
            {
                MessageBox.Show("You have not set a war target for this player!");
            }
        }

        public void SynchronizeOtherPalyerName()
        {
            OtherPlayerName.items.Clear();
            foreach (string name in Client.INSTANCE.OtherPlayer)
            {
                OtherPlayerName.item.Add(name);
            }
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
