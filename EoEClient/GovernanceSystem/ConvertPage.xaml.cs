using EoE.Client;
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

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// Convert.xaml 的交互逻辑
    /// </summary>
    public partial class ConvertPage : Window
    {
        
        public ConvertPage()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(inputNumber.Text,out int count)&& count>=0)
            {
                GameResourceType? origin =null;
                if (PrimaryField.SelectedItem.ToString() == "Silicon")
                {
                    origin = GameResourceType.Silicon;
                }else if (PrimaryField.SelectedItem.ToString() == "Copper")
                {
                    origin = GameResourceType.Copper;
                }else if (PrimaryField.SelectedItem.ToString() == "Iron")
                {
                    origin = GameResourceType.Iron;
                }else if (PrimaryField.SelectedItem.ToString() == "Aluminum")
                {
                    origin = GameResourceType.Aluminum;
                }
                else
                {
                    MessageBox.Show("Please select a primary field");
                }

                GameResourceType? convert = null;
                if (SecondaryField.SelectedItem.ToString() == "Electronic")
                {
                    convert = GameResourceType.Electronic;
                }
                if (SecondaryField.SelectedItem.ToString() == "Industrial")
                {
                    convert = GameResourceType.Industrial;
                }
                else
                {
                    MessageBox.Show("Please select a secondary field");
                    
                }
                if (origin != null && convert != null)
                {
                    Client.INSTANCE.GonveranceManager.FieldList.Filedconversion((GameResourceType)origin, count, (GameResourceType)convert, count);
                }
                
            }
            else
            {
                MessageBox.Show("Please input an positive value");
            }
           

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
