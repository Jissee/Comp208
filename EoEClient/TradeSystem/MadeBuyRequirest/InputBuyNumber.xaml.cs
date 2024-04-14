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

namespace WpfApp1.MadeBuyRequirest
{
    /// <summary>
    /// InputBuyNumber.xaml 的交互逻辑
    /// </summary>
    public partial class InputBuyNumber : Window
    {
        private string selectedPlayer;
        private string tradeType;
       
        public InputBuyNumber(string playerName, string type)
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
                SellItemWindow sellItemWindow = new SellItemWindow();
                sellItemWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入整数");
            }

            
        }
    }
}
