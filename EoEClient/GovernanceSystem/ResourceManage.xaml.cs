using EoE.GovernanceSystem;
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

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// ResourceManage.xaml 的交互逻辑
    /// </summary>
    public partial class ResourceManage : Window
    {
        public ResourceManage()
        {
            InitializeComponent();
            Population.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();

            Silicon.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Silicon).ToString();
            Copper.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Copper).ToString();
            Aluminum.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Aluminum).ToString();
            Iron.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Iron).ToString();
            Electronic.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Electronic).ToString();
            Industrial.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Industrial).ToString();

            SiliconBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Silicon).ToString();
            CopperBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Copper).ToString();
            AluminumBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Aluminum).ToString();
            IronBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Aluminum).ToString();
            ElectronicBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount (GameResourceType.Electronic).ToString();
            IndustrialBlock.Text = Client.INSTANCE.GonveranceManager.FieldList.GetFieldCount(GameResourceType.Industrial).ToString ();

            BattleArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount (GameResourceType.BattleArmy).ToString();
            InfoArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.InformativeArmy).ToString ();
            MechArmy.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.MechanismArmy).ToString ();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}