using System.Windows;
using System.Windows.Controls;

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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var dict = Client.INSTANCE.WarManager.ClientWarInformationList.WarInformationList;
                    if(!dict.ContainsKey(selectedWarName))
                    {
                        CheckWarDetail windowShowName = WindowManager.INSTANCE.GetWindows<CheckWarDetail>();
                        windowShowName.WarName.Text = selectedWarName;
                        WindowManager.INSTANCE.ShowWindows<CheckWarDetail>();
                        return;
                    }
                    ClientWarInformation info = dict[selectedWarName];
                    
                    CheckWarDetail window = WindowManager.INSTANCE.GetWindows<CheckWarDetail>();
                    window.WarName.Text = selectedWarName;
                    TextBox battleWe = window.BattleWe;
                    TextBox infoWe = window.InfoWe;
                    TextBox mechWe = window.MechWe;
                    TextBox battleLostWe = window.BattleLostWe;
                    TextBox infoLostWe = window.InfoLostWe;
                    TextBox mechLostWe = window.MechLostWe;
                    battleWe.Text = info.totalBattle.ToString();
                    infoWe.Text = info.totalInformative.ToString();
                    mechWe.Text = info.totalMechanism.ToString();
                    battleLostWe.Text = info.battleLost.ToString();
                    infoLostWe.Text = info.informativeLost.ToString();
                    mechLostWe.Text = info.mechanismLost.ToString();

                    TextBox battleEnemy = window.BattleEnemy;
                    TextBox infoEnemy = window.InfoEnemy;
                    TextBox mechEnemy = window.MechEnemy;
                    TextBox battleLostEnemy = window.BattleLostEnemy;
                    TextBox infoLostEnemy = window.InfoLostEnemy;
                    TextBox mechLostEnemy = window.MechLostEnemy;
                    battleEnemy.Text = info.enemyTotalBattle.ToString();
                    infoEnemy.Text = info.enemyTotalInformative.ToString();
                    mechEnemy.Text = info.enemyTotalMechanism.ToString();
                    battleLostEnemy.Text = info.enemyBattleLost.ToString();
                    infoLostEnemy.Text = info.enemyInformativeLost.ToString();
                    mechLostEnemy.Text = info.enemyMechanismLost.ToString();
                });
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
