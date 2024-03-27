using EoE.GovernanceSystem;
using EoE.Server.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public abstract class ArmyStack : ResourceStack
    {
        public abstract int AttackPower {  get; }
        public abstract int DefendPower {  get; }
        public ArmyStack(GameResourceType type, int count) : base(type, count)
        {
        }
    }
}
