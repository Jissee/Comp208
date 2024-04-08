using EoE.GovernanceSystem;
using EoE.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class InformativeArmyStack : ArmyStack
    {
        public override int Worth => 3;
        public override int MechanAttack => 0;

        public override int MechanDefense => 2;

        public override int BattleAttack => 1;
        public InformativeArmyStack(int count) : base(GameResourceType.InformativeArmy, count)
        {
        }
    }
}
