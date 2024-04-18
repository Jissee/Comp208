using EoE.GovernanceSystem;

namespace EoE.WarSystem.Interface
{
    public interface IArmy
    {
        int Consumption { get; }
        void AddBattle(ResourceStack battle);
        void AddMechanism(ResourceStack mechanism);
        void AddInformative(ResourceStack informative);
        int GetBattleCount();
        int GetInformativeCount();
        int GetMechanismCount();
        void AddConsumption(int count);
        void Clear();
        void DecreaseBattle(int count);
        void DecreaseMechanism(int count);
        void DecreaseInformative(int count);

        int CalculateMechaAttack();
        int CalculateMechaDefense();
        int CalculateBattleAttack();
    }
}