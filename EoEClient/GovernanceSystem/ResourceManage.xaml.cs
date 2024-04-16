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
            Blocks.Text = Client.INSTANCE.GonveranceManager.FieldList.TotalFieldCount.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
//maybe using pattern like"100+5" to show exist and new resources
//or using pattern like"100+0.5%" to show speed(same as new in each round) and total resources