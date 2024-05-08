using EoE.Governance;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EoE.Client.Governance
{
    /// <summary>
    /// Convert.xaml 的交互逻辑
    /// </summary>
    public partial class UpgradeFieldWindow : Window
    {

        public UpgradeFieldWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(inputNumber.Text, out int count) && count >= 0)
            {
                GameResourceType? origin = null;
                if (PrimaryField.SelectedItem == null)
                {
                    MessageBox.Show("Please select a primary field");
                }
                else
                {
                    ListBoxItem item = PrimaryField.SelectedItem as ListBoxItem;

                    if (item.Content.ToString() == "Silicon")
                    {
                        origin = GameResourceType.Silicon;
                    }
                    else if (item.Content.ToString() == "Copper")
                    {
                        origin = GameResourceType.Copper;
                    }
                    else if (item.Content.ToString() == "Iron")
                    {
                        origin = GameResourceType.Iron;
                    }
                    else if (item.Content.ToString() == "Aluminum")
                    {
                        origin = GameResourceType.Aluminum;
                    }
                }



                GameResourceType? convert = null;
                if (PrimaryField.SelectedItem == null)
                {
                    MessageBox.Show("Please select a secondary field");
                }
                else
                {
                    ListBoxItem item = SecondaryField.SelectedItem as ListBoxItem;
                    if (item.Content.ToString() == "Electronic")
                    {
                        convert = GameResourceType.Electronic;
                    }
                    else if (item.Content.ToString() == "Industrial")
                    {
                        convert = GameResourceType.Industrial;
                    }
                }





                if (origin != null && convert != null)
                {
                    Client.INSTANCE.GonveranceManager.FieldList.FieldConversion((GameResourceType)origin, count, (GameResourceType)convert, count);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
