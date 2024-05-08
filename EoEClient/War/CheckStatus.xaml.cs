using System.Windows;

namespace EoE.Client.War
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
            if (window.checkStatusListBoxWarName.SelectedItem != null)
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
