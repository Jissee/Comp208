using EoE.Client.GovernanceSystem;
using EoE.Client.Login;
using EoE.Client.TradeSystem;
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

namespace EoE.Client.TradeSystem
{
    /// <summary>
    /// SellAndBuy2.xaml 的交互逻辑
    /// </summary>
    public partial class SellAndBuy2 : Window
    {
       
        int[] sellValues = new int[6];
        int[] buyValues = new int[6];
        public SellAndBuy2()
        {
            InitializeComponent();
            InitializeTextBoxes();
        }

        private void InitializeTextBoxes()
        {
            
            for (int i = 1; i <= 6; i++)
            {
                TextBox sellTextBox = FindName($"Sell{i}") as TextBox;
                TextBox buyTextBox = FindName($"Buy{i}") as TextBox;

                if (sellTextBox != null)
                {
                    sellTextBox.TextChanged += SellTextBox_TextChanged;
                }

                if (buyTextBox != null)
                {
                    buyTextBox.TextChanged += BuyTextBox_TextChanged;
                }
            }
        }

        private void SellTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int index = int.Parse(textBox.Name.Substring(4)) - 1;
            if (int.TryParse(textBox.Text, out int value))
            {
                sellValues[index] = value;
            }
            else
            {
                sellValues[index] = 0;
            }
        }

        private void BuyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int index = int.Parse(textBox.Name.Substring(3)) - 1;
            if (int.TryParse(textBox.Text, out int value))
            {
                buyValues[index] = value;
            }
            else
            {
                buyValues[index] = 0;
            }
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            bool sellFilled = Array.Exists(sellValues, v => v != 0);
            bool buyFilled = Array.Exists(buyValues, v => v != 0);
            if (sellFilled || buyFilled)
            {
                MessageBox.Show("Sucefully!");
                this.Hide();
            }
            else
            {
                MessageBox.Show("There must be at least one integer between sell and buy！");
                return;
            }
            this.Hide();

        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MainTradeWindow>();
            this.Hide();

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
