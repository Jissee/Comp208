using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
