using EoE.Network.Packets.GonverancePacket.Record;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EoE.Client.Governance
{
    /// <summary>
    /// ExpolreField.xaml 的交互逻辑
    /// </summary>
    public partial class SetExploreWindow : Window
    {

        public SetExploreWindow()
        {
            InitializeComponent();
            availablePopulation.Text = Client.INSTANCE.GonveranceManager.PopManager.AvailablePopulation.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(number.Text, out int count) && count >= 0)
            {
                Client.INSTANCE.GonveranceManager.SetExploration(count);
                WindowManager.INSTANCE.UpdatePopulation();
                WindowManager.INSTANCE.UpdateResources();
            }
            else
            {
                MessageBox.Show("Please enter a number greater than or equal to 1.");
            }

        }

        public void SynchronizePopulation(PopulationRecord populationRecord)
        {
            availablePopulation.Text = populationRecord.availablePopulation.ToString();
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
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
