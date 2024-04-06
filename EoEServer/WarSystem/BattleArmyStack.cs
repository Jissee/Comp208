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
        public override int Worth => 1;
        public override int MechanAttack => 0;

        public override int MechanDefense => 0;

        public override int BattleAttack => 2;

        public BattleArmyStack(int count) : base(GameResourceType.BattleArmy, count)
        {
        }
    }
}
