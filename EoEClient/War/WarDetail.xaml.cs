using EoE.Network.Packets.WarPacket;
using EoE.War;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EoE.Client.War
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
            int result;
            if (int.TryParse(Silicon.Text, out result))
            {
                target.SiliconClaim = result;
            }
            if (int.TryParse(Copper.Text, out result))
            {
                target.CopperClaim = result;
            }
            if (int.TryParse(Iron.Text, out result))
            {
                target.IronClaim = result;
            }
            if (int.TryParse(Aluminum.Text, out result))
            {
                target.AluminumClaim = result;
            }
            if (int.TryParse(Electronic.Text, out result))
            {
                target.ElectronicClaim = result;
            }
            if (int.TryParse(Industrial.Text, out result))
            {
                target.IndustrialClaim = result;
            }
            if (int.TryParse(Population.Text, out result))
            {
                target.PopClaim = result;
            }
            if (int.TryParse(Fields.Text, out result))
            {
                target.FieldClaim = result;
            }
            if (OtherPlayerName.SelectedItem != null)
            {
                WarTargetPacket packet = new WarTargetPacket(OtherPlayerName.SelectedItem.ToString()!, target);
                Client.INSTANCE.SendPacket(packet);
                MessageBox.Show("You have successfully submitted.");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = OtherPlayerName.SelectedItem.ToString()!;
            if (((ClientWarTargetList)Client.INSTANCE.WarManager.ClientWarTargetList).WarTargetList.ContainsKey(name))
            {
                WarQueryTargetPacket packet = new WarQueryTargetPacket(name, new WarTarget());
                Client.INSTANCE.SendPacket(packet);
            }
            else
            {
                MessageBox.Show("You have not set a war target for this player!");
            }
        }

        public void SynchronizeOtherPlayerList()
        {
            OtherPlayerName.Items.Clear();
            foreach (string name in Client.INSTANCE.OtherPlayer)
            {
                OtherPlayerName.Items.Add(name);
            }
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
