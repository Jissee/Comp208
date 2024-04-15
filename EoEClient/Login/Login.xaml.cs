using EoE.Client.TradeSystem;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WarSystem;

namespace EoE.Client
{
 
    
    public partial class Login : Window
    {
         public event EventHandler NavigateToSelectPage;
        [STAThread]
        public static void Main()
        {
            //从这个地方开始运行程序
            Application app = new Application();
            app.Run(new Login());
            //app.Run(new WarMainPage());
        }

        public Login()
        {
            InitializeComponent();
        }

        private void Quary_Click(object sender, RoutedEventArgs e)
        {
            GameAgreement gameAgreementWindow = new GameAgreement();
            gameAgreementWindow.Owner = this; 
            gameAgreementWindow.ShowDialog();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServerAddress.Text))
            {
                MessageBox.Show("Please enter the server address!");
                return;
            }

            if (string.IsNullOrWhiteSpace(Username.Text))
            {
                MessageBox.Show("Please enter the username!");
                return;
            }

            if (agreeCheckBox.IsChecked != true)
            {
                MessageBox.Show("Please agree to the game agreement first!");
                return;
            }

            List<string> players = new List<string>();
            players.Add(Username.Text);

            EnterGamePage enterGamePage = new EnterGamePage(players, Username.Text);
            enterGamePage.Show();
            this.Close();
        }


            private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            bool isChecked = agreeCheckBox.IsChecked ?? false;
            Connect.IsEnabled = isChecked;
        }

        //实时更新人数
        private void UpdateConnectButtonState()
        {
            bool isChecked = agreeCheckBox.IsChecked ?? false;

            if (isChecked)
            {
                bool isServerAddressValid = !string.IsNullOrWhiteSpace(ServerAddress.Text);
                bool isUsernameValid = !string.IsNullOrWhiteSpace(Username.Text);

                Connect.IsEnabled = isServerAddressValid && isUsernameValid;
            }
            else
            {
                Connect.IsEnabled = false;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
