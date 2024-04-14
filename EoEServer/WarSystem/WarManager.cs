using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarManager : IWarManager, ITickable
    {
        public Dictionary<string, IWar> WarDict { get; private set; } = new Dictionary<string, IWar>();
        public WarManager() { }
        public void DeclareWar(IWar war)
        {
            WarDict.Add(war.WarName, war);
            war.SetWarManager(this);
        }
        public void RemoveWar(IWar war)
        {
            if (WarDict.ContainsValue(war))
            {
                WarDict.Remove(war.WarName);
            }
        }
        public void PlayerLose(IPlayer player)
        {
            foreach (var kvp in WarDict)
            {
                IWar war = kvp.Value;
                IWarParty attackers = war.Attackers;
                IWarParty defenders = war.Defenders;
                if (attackers.Contains(player))
                {
                    attackers.PlayerLose(player);
                }
                if (defenders.Contains(player))
                {
                    attackers.PlayerLose(player);
                }
            }
        }
        public void Tick()
        {

        }
    }
}
