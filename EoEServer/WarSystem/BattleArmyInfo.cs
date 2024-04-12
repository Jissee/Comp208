using EoE.GovernanceSystem;
using EoE.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class BattleArmyInfo : ArmyInfo
    {
        public override int Worth => 1;
        public override int MechanAttack => 0;

        public override int MechanDefense => 0;

        public override int BattleAttack => 2;

    }
}
