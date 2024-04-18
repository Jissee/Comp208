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

namespace EoE.Client.WarSystem
{
    /// <summary>
    /// CheckStatus.xaml 的交互逻辑
    /// </summary>
    public partial class CheckStatus : Window
    {
        public static string selectedWarName;
        public CheckStatus()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CheckStatus window = WindowManager.INSTANCE.GetWindows<CheckStatus>();
            if(window.checkStatusListBoxWarName.SelectedItem != null)
            {
                selectedWarName = checkStatusListBoxWarName.SelectedItem.ToString()!;
                WindowManager.INSTANCE.ShowWindows<CheckWarDetail>();
            }
            else
            {
                MessageBox.Show("You do not have any war or you have not selected any war!");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
