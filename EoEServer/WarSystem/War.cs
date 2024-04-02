using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War : ITickable
    {
        private WarParty attackers;
        private WarParty defenders;
        private WarTarget attackersTarget;
        private WarTarget defendersTarget;

        public War(WarParty attackers, WarParty defenders)
        {
            this.attackers = attackers;
            this.defenders = defenders;
        }
        public void SetAttackersWarTarget(WarTarget warTarget)
        {
            attackersTarget = warTarget;
        }
        public void SetDefendersWarTarget(WarTarget warTarget) 
        {
            defendersTarget = warTarget;
        }
        public void Tick()
        {
        }
    }
}
