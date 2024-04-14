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
    /// AllocateArmy.xaml 的交互逻辑
    /// </summary>
    public partial class AllocateArmy : Window
    {
        public AllocateArmy()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
//只允许输入数字
//right hand side textbox should show the value of middle plus left hand
//