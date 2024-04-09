using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War : ITickable
    {
        public string WarName { get; init; }
        private WarParty attackers;
        private WarParty defenders;
        private WarTarget attackersTarget;
        private WarTarget defendersTarget;

        public War(WarParty attackers, WarParty defenders, string warName)
        {
            this.attackers = attackers;
            this.defenders = defenders;
            WarName = warName;
        }
        public void SetAttackersWarTarget(WarTarget warTarget)
        {
            attackersTarget = warTarget;
        }
        public void SetDefendersWarTarget(WarTarget warTarget) 
        {
            defendersTarget = warTarget;
        }
        public void End()
        {
        }
        public void Tick()
        {
        }

    }
}
