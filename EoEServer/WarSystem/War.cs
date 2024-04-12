using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War : ITickable
    {
        public string WarName { get; private set; }
        public WarParty attackers { get; private set; }
        public WarParty defenders { get; private set; }
        public WarTarget attackersTarget { get; private set; }
        public WarTarget defendersTarget { get; private set; }

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
