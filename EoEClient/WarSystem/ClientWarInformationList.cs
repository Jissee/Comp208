using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
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
            //todo show things to windows

        }
    }
}