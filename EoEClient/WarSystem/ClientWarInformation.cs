using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public struct ClientWarInformation
    {
        private int totalBattle;
        private int totalInformative;
        private int totalMechanism;
        private int battleLost;
        private int informativeLost;
        private int mechanismLost;

        private int enemyTotalBattle;
        private int enemyTotalInformative;
        private int enemyTotalMechanism;
        private int enemyBattleLost;
        private int enemyInformativeLost;
        private int enemyMechanismLost;
        public ClientWarInformation(
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
            this.totalBattle = totalBattle;
            this.totalInformative = totalInformative;
            this.totalMechanism = totalMechanism;
            this.battleLost = battleLost;
            this.informativeLost = informativeLost;
            this.mechanismLost = mechanismLost;
            this.enemyTotalBattle = enemyTotalBattle;
            this.enemyTotalInformative = enemyTotalInformative;
            this.enemyTotalMechanism = enemyTotalMechanism;
            this.enemyBattleLost = enemyBattleLost;
            this.enemyInformativeLost = enemyInformativeLost;
            this.enemyMechanismLost = enemyMechanismLost;
        }
    }
}
