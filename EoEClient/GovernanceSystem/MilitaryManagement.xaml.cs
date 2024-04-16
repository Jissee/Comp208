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
    /// MilitaryManagement.xaml 的交互逻辑
    /// </summary>
    public partial class MilitaryManagement : Window
    {
        public MilitaryManagement()
        {
            InitializeComponent();
            battleafter.Text = battlebefore.Text + battlechange.Text;
            techafter.Text = techbefore .Text + techchange.Text;
            mechafter.Text = mechbefore .Text + mechchange.Text;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void battlechange_TextChanged(object sender, TextChangedEventArgs e)
        {
            battleafter.Text = battlebefore.Text + battlechange.Text;
        }

        private void techchange_TextChanged(object sender, TextChangedEventArgs e)
        {
            techafter.Text = techbefore.Text + techchange.Text;
        }

        private void mechchange_TextChanged(object sender, TextChangedEventArgs e)
        {
            mechafter.Text = mechbefore.Text + mechchange.Text;
        }
    }
}
