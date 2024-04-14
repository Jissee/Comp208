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

namespace WarSystem
{
    /// <summary>
    /// DeclareWar.xaml 的交互逻辑
    /// </summary>
    public partial class DeclareWar : Window
    {
        public DeclareWar()
        {
            InitializeComponent();
            dockPanel.Visibility = Visibility.Hidden;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (listBox1.SelectedItem != null )
            {
                // 执行提交操作
                this.Hide();
                MessageBox.Show("You can manage your army in next page or find the page in Check status of War!");
                AllocateArmy allocateArmy = new AllocateArmy();
                allocateArmy .Show();
                
            }
            else
            {
                MessageBox.Show("Please select an enemy from list.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReviewDiplomatic reviewDiplomatic = new ReviewDiplomatic();
            reviewDiplomatic.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dockPanel.Visibility = Visibility.Visible;
        }
    }
}
//no alliance with player is allowed, but a message box for notice is need?