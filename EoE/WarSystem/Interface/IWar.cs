using EoE.Server.WarSystem;

namespace EoE.WarSystem.Interface
{
    public interface IWar : ITickable
    {
        public string WarName { get; }
        public IWarParty Attackers { get; }
        public IWarParty Defenders { get; }
        public WarTarget AttackersTarget { get; }
        public WarTarget DefendersTarget { get; }
        //void SetWarManager(IWarManager manager);
        public void SetAttackersWarTarget(WarTarget warTarget);
        public void SetDefendersWarTarget(WarTarget warTarget);
        IWarParty GetWarPartyOfPlayer(IPlayer player);
        IWarParty GetWarEnemyPartyOfPlayer(IPlayer player);
        void End(IWarParty? defeated);
    }
}
