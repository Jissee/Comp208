using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class ArmyInfo
    {
        public virtual int Worth { get; }
        public virtual int MechanAttack { get; }

        public virtual int MechanDefense { get; }

        public virtual int BattleAttack { get; }
    }
}
