using EoE.Governance;
using EoE.Network.Packets.GonverancePacket.Record;
using System.Windows;

namespace EoE.Client.Governance
{
    /// <summary>
    /// ResourceManage.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceInformationWindow : Window
    {
        public ResourceInformationWindow()
        {
            InitializeComponent();
            Population.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();
            AvailablePopulation.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();

            Silicon.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Silicon).ToString();
            Copper.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Copper).ToString();
            Aluminum.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Aluminum).ToString();
            Iron.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Iron).ToString();
            Electronic.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Electronic).ToString();
            Industrial.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Industrial).ToString();

            SiliconField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Silicon).ToString();
            CopperField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Copper).ToString();
            AluminumField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Aluminum).ToString();
            IronField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Aluminum).ToString();
            ElectronicField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Electronic).ToString();
            IndustrialField.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Industrial).ToString();

            BattleArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.BattleArmy).ToString();
            InfoArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.InformativeArmy).ToString();
            MechArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.MechanismArmy).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public void SynchronizeResources(ResourceListRecord record)
        {
            Silicon.Text = record.siliconCount.ToString();
            Copper.Text = record.copperCount.ToString();
            Aluminum.Text = record.aluminumCount.ToString();
            Iron.Text = record.ironCount.ToString();
            Electronic.Text = record.electronicCount.ToString();
            Industrial.Text = record.industrialCount.ToString();

            BattleArmy.Text = record.battleArmyCount.ToString();
            InfoArmy.Text = record.informativeArmyCount.ToString();
            MechArmy.Text = record.mechanismArmyCount.ToString();
        }

        public void SynchronizeFields(FieldListRecord record)
        {
            SiliconField.Text = record.siliconFieldCount.ToString();
            CopperField.Text = record.copperFieldCount.ToString();
            AluminumField.Text = record.aluminumFieldCount.ToString();
            IronField.Text = record.ironFieldCount.ToString();
            ElectronicField.Text = record.electronicFieldCount.ToString();
            IndustrialField.Text = record.industrialFieldCount.ToString();
        }

        public void SynchronizePopulation(PopulationRecord record)
        {
            Population.Text = (record.siliconPop + record.copperPop + record.ironPop + record.aluminumPop + record.electronicPop + record.industrialPop + record.availablePopulation).ToString();
            AvailablePopulation.Text = record.availablePopulation.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}