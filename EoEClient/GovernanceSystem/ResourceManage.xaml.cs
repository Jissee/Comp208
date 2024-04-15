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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
//maybe using pattern like"100+5" to show exist and new resources
//or using pattern like"100+0.5%" to show speed(same as new in each round) and total resources