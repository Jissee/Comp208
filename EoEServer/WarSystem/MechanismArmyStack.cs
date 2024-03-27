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
        public override int AttackPower { get => 5; }
        public override int DefendPower { get => 2; }
        public MechanismArmyStack(GameResourceType type, int count) : base(type, count)
        {
        }
    }
}
