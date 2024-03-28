using EoE.Server.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarParty
    {
        private Dictionary<ServerPlayer,Army> armies;
        public int WarWidth => 60 / Count;
        public int Count => armies.Count;

        public void AddPlayer(ServerPlayer player)
        {
            Army army = new Army(this);
            player.AddArmy(army);
            armies.Add(player, army);
        }
    }
}
