using EoE.Client.War;

namespace EoE.War.Interface
{
    public interface IClientWarInformationList
    {
        public Dictionary<string, ClientWarInformation> WarInformationList { get; }
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
