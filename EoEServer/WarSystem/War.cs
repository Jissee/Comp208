using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War
    {
        private WarParty attackers;
        private WarParty defenders;
        public War(WarParty attackers, WarParty defenders)
        {
            this.attackers = attackers;
            this.defenders = defenders;
        }
    }
}
