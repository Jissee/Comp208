using EoE.War.Interface;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.War
{
    public class ClientWarInformationList : IClientWarInformationList
    {
        public Dictionary<string, ClientWarInformation> WarInformationList { get; set; }
        public ClientWarInformationList()
        {
            WarInformationList = new Dictionary<string, ClientWarInformation>();
        }
        public void ChangeWarInformationList(
            string warName,
            int totalBattle,
            int totalInformative,
            int totalMechanism,
            int battleLost,
            int informativeLost,
            int mechanismLost,

            int enemyTotalBattle,
            int enemyTotalInformative,
            int enemyTotalMechanism,
            int enemyBattleLost,
            int enemyInformativeLost,
            int enemyMechanismLost
            )
        {
            ClientWarInformation warInformation = new ClientWarInformation(
            totalBattle,
            totalInformative,
            totalMechanism,
            battleLost,
            informativeLost,
            mechanismLost,

            enemyTotalBattle,
            enemyTotalInformative,
            enemyTotalMechanism,
            enemyBattleLost,
            enemyInformativeLost,
            enemyMechanismLost
            );
            if (WarInformationList.ContainsKey(warName))
            {
                WarInformationList[warName] = warInformation;
            }
            else
            {
                WarInformationList.Add(warName, warInformation);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckWarDetail window = WindowManager.INSTANCE.GetWindows<CheckWarDetail>();
                TextBox battleWe = window.BattleWe;
                TextBox infoWe = window.InfoWe;
                TextBox mechWe = window.MechWe;
                TextBox battleLostWe = window.BattleLostWe;
                TextBox infoLostWe = window.InfoLostWe;
                TextBox mechLostWe = window.MechLostWe;
                battleWe.Text = totalBattle.ToString();
                infoWe.Text = totalInformative.ToString();
                mechWe.Text = totalMechanism.ToString();
                battleLostWe.Text = battleLost.ToString();
                infoLostWe.Text = informativeLost.ToString();
                mechLostWe.Text = mechanismLost.ToString();

                TextBox battleEnemy = window.BattleEnemy;
                TextBox infoEnemy = window.InfoEnemy;
                TextBox mechEnemy = window.MechEnemy;
                TextBox battleLostEnemy = window.BattleLostEnemy;
                TextBox infoLostEnemy = window.InfoLostEnemy;
                TextBox mechLostEnemy = window.MechLostEnemy;
                battleEnemy.Text = enemyTotalBattle.ToString();
                infoEnemy.Text = enemyTotalInformative.ToString();
                mechEnemy.Text = enemyTotalMechanism.ToString();
                battleLostEnemy.Text = enemyBattleLost.ToString();
                infoLostEnemy.Text = enemyInformativeLost.ToString();
                mechLostEnemy.Text = enemyMechanismLost.ToString();
            });
        }
    }
}