using EoE.Client;
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
   
    public partial class SetGameWindow : Window
    {
        bool Confirm3Click = false;
        bool amount = false;
        bool round = false;
       
        public SetGameWindow()
        {
            InitializeComponent();
            
        }

        
        private void Confirm_Click_1(object sender, RoutedEventArgs e)
        {
            if (Confirm3Click)
            {
                MainGamePage mainGamePage = new MainGamePage();
                mainGamePage.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("You haven't changed any values. If you confirm, please submit again.");
                Confirm3Click = true;
            }
           
        }

        private void Confirm3_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = SetResource.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                // 发送选择的选项给服务端
                string selectedOption = selectedItem.Content.ToString();
                Console.WriteLine("Options to send to the server: " + selectedOption);
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            round = true;
            if (selectedValueTextBox != null) 
            {
                selectedValueTextBox.Text = ((int)e.NewValue).ToString();
            }
            if(amount)
            {
                Confirm3Click = true;
            }
        }

        private void SetResource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            amount = true;
            if(round)
            {
                Confirm3Click = true;
            }
        }
    }
}
