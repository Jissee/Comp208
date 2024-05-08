using EoE.Governance;
using EoE.Network.Packets.GonverancePacket.Record;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EoE.Client.Governance
{
    /// <summary>
    /// MilitaryManagement.xaml 的交互逻辑
    /// </summary>
    public partial class MilitaryManagementWindow : Window
    {
        public MilitaryManagementWindow()
        {
            InitializeComponent();
            battlebefore.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.BattleArmy).ToString();
            infobefore.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.InformativeArmy).ToString();
            mechbefore.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.MechanismArmy).ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

        }

        private void battlechange_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void techchange_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void mechchange_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void SynchronizeResources(ResourceListRecord record)
        {
            battlebefore.Text = record.battleArmyCount.ToString();
            infobefore.Text = record.informativeArmyCount.ToString();
            mechbefore.Text = record.mechanismArmyCount.ToString();
        }
        private void Submit3_Click(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(battlechange.Text, out int battle) && battle >= 0) && (int.TryParse(informativeChange.Text, out int informative) && informative >= 0) && (int.TryParse(mechchange.Text, out int mech) && mech >= 0))
            {
                Client.INSTANCE.GonveranceManager.SyntheticArmy(GameResourceType.BattleArmy, battle);
                Client.INSTANCE.GonveranceManager.SyntheticArmy(GameResourceType.InformativeArmy, informative);
                Client.INSTANCE.GonveranceManager.SyntheticArmy(GameResourceType.MechanismArmy, mech);
            }
            else
            {
                MessageBox.Show("Please enter a number greater than or equal to 0.");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
