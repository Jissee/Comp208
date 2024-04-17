using EoE.GovernanceSystem;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            battlebefore.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.BattleArmy).ToString();
            techbefore.Text = Client.INSTANCE.GonveranceManager.ResourceList.GetResourceCount(GameResourceType.InformativeArmy).ToString();
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

        private void Submit3_Click(object sender, RoutedEventArgs e)
        {
            if ((int.TryParse(battlechange.Text, out int value1) && value1 >= 0) && (int.TryParse(techchange.Text, out int value2) && value2 >= 0) && (int.TryParse(mechchange.Text, out int value3) && value3 >= 0))
            {
                Client.INSTANCE.GonveranceManager
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
