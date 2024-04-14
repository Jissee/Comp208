using EoE.Client.TradeSystem;
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

namespace WpfApp1
{
    /// <summary>
    /// SearchTransationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTransationWindow : Window
    {
        public SearchTransationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = findBox.Text;
            string str2 = (String)((ListBoxItem)listbox.SelectedItem).Content;
            MessageBox.Show(str2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainTradePage mainTradePage = new MainTradePage();
            mainTradePage.Show();
            this.Close();
        }
    }
}
