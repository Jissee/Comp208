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

        }


        public void getValue()
        {

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
         
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

        private void res1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
