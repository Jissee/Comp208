using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War : IWar
    {
        public string WarName { get; private set; }
        public IWarParty Attackers { get; private set; }
        public IWarParty Defenders { get; private set; }
        public IWarTarget AttackersTarget { get; private set; }
        public IWarTarget DefendersTarget { get; private set; }
        public IWarManager WarManager { get; private set; }

        public War(IWarParty attackers, IWarParty defenders, string warName)
        {
            this.Attackers = attackers;
            this.Defenders = defenders;
            WarName = warName;
            attackers.SetWar(this);
            defenders.SetWar(this);
        }
        public void SetWarManager(IWarManager manager)
        {
            this.WarManager = manager;
        }
        public IWarParty GetWarPartyOfPlayer(IPlayer player)
        {
            if(Attackers.Contains(player))
            {
                return Attackers;
            }
            else if (Defenders.Contains(player))
            {
                return Defenders;
            }
            throw new Exception("No player in this war");
        }
        public void SetAttackersWarTarget(IWarTarget warTarget)
        {
            AttackersTarget = warTarget;
        }
        public void SetDefendersWarTarget(IWarTarget warTarget) 
        {
            DefendersTarget = warTarget;
        }
        public void End(IWarParty defeated)
        {
            
            if (defeated == Attackers)
            {
                //DefendersTarget;
            }
            else
            {
                //AttackersTarget;
            }
            WarManager.RemoveWar(this);
        }
        public void Tick()
        {
            
        }

    }
}
