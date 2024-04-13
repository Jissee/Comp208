using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class TruceTreaty : Treaty, ITickableTreaty
    {
        private int remainingTime;
        public TruceTreaty(ServerPlayer firstParty, ServerPlayer secondParty, int time) : base(firstParty, secondParty)
        {
            remainingTime = time;
        }
        public bool IsAvailable()
        {
            return remainingTime > 0;
        }

        public void Tick()
        {
            remainingTime--;
        }
    }
}
