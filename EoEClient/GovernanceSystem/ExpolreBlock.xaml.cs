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

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// ExpolreBlock.xaml 的交互逻辑
    /// </summary>
    public partial class ExpolreBlock : Window
    {
        private static ExpolreBlock instance;
        public static ExpolreBlock INSTANCE 
        { 
            get 
            { 
                if(instance == null || !instance.IsLoaded)
                {
                    instance = new ExpolreBlock();
                }
                return instance;
            } 
        }
        public ExpolreBlock()
        {
            InitializeComponent();
            Population.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(number.Text, out int value) && value >= 1)
            {
                MessageBox.Show("You have successfully sent people to explore.");
            }
            else
            {
                MessageBox.Show("Please enter a number greater than or equal to 1.");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
