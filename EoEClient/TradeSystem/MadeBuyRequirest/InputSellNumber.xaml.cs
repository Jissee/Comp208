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
using WpfApp1.MadeBuyRequirest;

namespace WpfApp1
{
    /// <summary>
    /// InputSellNumber.xaml 的交互逻辑
    /// </summary>
    public partial class InputSellNumber : Window
    {

        private string selectedPlayer;
        private string tradeType;

        public InputSellNumber(string playerName, string type)
        {
            InitializeComponent();
            selectedPlayer = playerName;
            tradeType = type;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string inputText = textBox.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string quantityText = InputNumber.Text;

            if (int.TryParse(quantityText, out int quantity))
            {
                ConfirmRequirestWindow confirmRequirestWindow = new ConfirmRequirestWindow();
                confirmRequirestWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入整数");
            }
            this.Close();
        }
    }
}
