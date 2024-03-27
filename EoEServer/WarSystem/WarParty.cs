using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarParty
    {
        private ServerPlayer[] players = new ServerPlayer[5];
        public int Count { get; private set; }

        public void AddPlayer(ServerPlayer player)
        {
            players[Count++] = player;
        }
        public ServerPlayer this[int index]
        {
            get { return players[index]; }
        }
    }
}
