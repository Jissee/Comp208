using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class InformativeArmyStack : ArmyStack
    {
        public override int AttackPower { get => 1; }
        public override int DefendPower { get => 4; }
        public InformativeArmyStack(GameResourceType type, int count) : base(type, count)
        {
        }
    }
}
