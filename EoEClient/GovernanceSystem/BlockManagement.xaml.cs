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
using EoE.Client.WarSystem;
using EoE.GovernanceSystem;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// BlockManagement.xaml 的交互逻辑
    /// </summary>
    public partial class BlockManagement : Window
    {
        
        public BlockManagement()
        {
            InitializeComponent();
            Silicon.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Silicon).ToString();
            Copper.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Copper).ToString();
            Aluminum.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Aluminum).ToString();
            Iron.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Iron).ToString();
            Electronic.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Electronic).ToString();
            Industrial.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.Industrial).ToString();

            SiliconPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Silicon).ToString();
            CopperPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Copper).ToString();
            AlumiumPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Aluminum).ToString();
            IronPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Iron).ToString();
            ElectronPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Electronic).ToString();
            IndustrialPeople.Text = Client.INSTANCE.GonveranceManager.PopManager.GetPopAllocCount(GameResourceType.Industrial).ToString();

            population.Text = Client.INSTANCE .GonveranceManager.PopManager.TotalPopulation.ToString()+"/"+ Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(SiliconAllocate.Text, out int silicon) && silicon >= 0)
                && (int.TryParse(CopperAllocate.Text, out int copper) && copper >= 0)
                && (int.TryParse(IronAllocate.Text, out int iron) && iron >= 0) 
                && (int.TryParse(AluminumAllocate.Text, out int aluminum) && aluminum >= 0) 
                && (int.TryParse(ElectronicAllocate.Text, out int electronic) && electronic >= 0)
                && (int.TryParse(IndustrialAllocate.Text, out int industrial) && industrial >= 0))
            {
                Client.INSTANCE.GonveranceManager.PopManager.SetAllocation(silicon,copper, iron,aluminum,electronic,industrial);
            }
            else
            {
                MessageBox.Show("Please enter a number greater than or equal to 0.");
            }
        }
        public void SynchronizePopulation(PopulationRecord record)
        {
            SiliconPeople.Text = record.siliconPop.ToString();
            CopperPeople.Text = record.copperPop.ToString();
            IronPeople.Text = record.ironPop.ToString();
            AlumiumPeople.Text = record.aluminumPop.ToString();
            ElectronPeople.Text = record.electronicPop.ToString();
            IndustrialPeople.Text = record.industrailPop.ToString();
            population.Text = record.availablePopulation.ToString();
        }
        public void SynchronizeFields(FieldListRecord record)
        {
            Silicon.Text = record.siliconFieldCount.ToString();
            Copper.Text = record.copperFieldCount.ToString();
            Iron.Text = record.ironFieldCount.ToString();
            Aluminum.Text = record.aluminumFieldCount.ToString();
            Electronic.Text = record.electronicFieldCount.ToString();
            Industrial.Text = record.industrialFieldCount.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
 