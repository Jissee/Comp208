using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class BattleArmyStack : ArmyStack
    {
        public override int AttackPower { get => 2; }
        public override int DefendPower { get => 1; }

        public BattleArmyStack(GameResourceType type, int count) : base(type, count)
        {
        }
    }
}
