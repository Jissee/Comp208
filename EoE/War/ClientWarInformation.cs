namespace EoE.Client.War
{
    public struct ClientWarInformation
    {
        public int totalBattle;
        public int totalInformative;
        public int totalMechanism;
        public int battleLost;
        public int informativeLost;
        public int mechanismLost;

        public int enemyTotalBattle;
        public int enemyTotalInformative;
        public int enemyTotalMechanism;
        public int enemyBattleLost;
        public int enemyInformativeLost;
        public int enemyMechanismLost;
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
