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
using EoE.Client.WarSystem;

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
            this.Closing += BlockManagement_Closing;
        }

        private void BlockManagement_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
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
    }
}
