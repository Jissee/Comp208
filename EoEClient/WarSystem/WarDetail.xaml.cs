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
    /// WarDetail.xaml 的交互逻辑
    /// </summary>
    public partial class WarDetail : Window
    {
        public WarDetail()
        {
            InitializeComponent();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You have successfully submitted.");
        }
    }
}
//name will have a formal parttern, but user can change it with own mind
//number word is allowed, caseintensive, no special signal