namespace EoE.WarSystem.Interface
{
    public interface IClientWarInformationList
    {
        void ChangeWarInformationList(
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
            );
    }
}
