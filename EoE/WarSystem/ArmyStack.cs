using EoE.GovernanceSystem;

namespace EoE.WarSystem
{
    public abstract class ArmyStack
    {
        public abstract int Worth { get; }
        public abstract int MechanAttack { get; }
        public abstract int MechanDefense { get; }
        public abstract int BattleAttack { get; }

        public ArmyStack(GameResourceType type, int count)
        {
        }
    }
}
