using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class MechanismArmyStack : ArmyStack
    {
        public override int Worth => 4;
        public override int MechanAttack => 2;

        public override int MechanDefense => 0;

        public override int BattleAttack => 0;

        public MechanismArmyStack(GameResourceType type, int count) : base(type, count)
        {
        }

    }
}
