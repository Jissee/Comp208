namespace EoE.Server.WarSystem
{
    public abstract class ArmyInfo
    {
        public abstract int Worth { get; }
        public abstract int MechanAttack { get; }
        public abstract int MechanDefense { get; }
        public abstract int BattleAttack { get; }
    }
}
