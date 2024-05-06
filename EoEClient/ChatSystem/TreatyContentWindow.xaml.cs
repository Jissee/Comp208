using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.ChatSystem
{
    /// <summary>
    /// TreatyContent.xaml 的交互逻辑
    /// </summary>
    public partial class TreatyContentWindow : Window
    {
        public TreatyContentWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
