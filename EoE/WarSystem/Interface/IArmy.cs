using EoE.GovernanceSystem;

namespace EoE.WarSystem.Interface
{
    public interface IArmy
    {
        ResourceStack Battle { get; set; }
        ResourceStack Informative { get; set; }
        ResourceStack Mechanism { get; set; }
        int Consumption { get; }
        void AddBattle(ResourceStack battle);
        void AddMechanism(ResourceStack mechanism);
        void AddInformative(ResourceStack informative);
        void AddConsumption(int count);

        void DecreaseBattle(int count);
        void DecreaseMechanism(int count);
        void DecreaseInformative(int count);

        int CalculateMechaAttack();
        int CalculateMechaDefense();
        int CalculateBattleAttack();
    }
}