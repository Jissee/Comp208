using EoE.GovernanceSystem;
using EoE.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class InformativeArmyInfo : ArmyInfo
    {
        public override int Worth => 5;
        public override int MechanAttack => 1;

        public override int MechanDefense => 2;

        public override int BattleAttack => 0;
    }
}
