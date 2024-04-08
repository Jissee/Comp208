using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class ProtectiveTreaty : Treaty
    {
        public ProtectiveTreaty(ServerPlayer firstParty, ServerPlayer secondParty) : base(firstParty, secondParty)
        {
        }
        public void ConsumeRecourse()
        {
            
        }
        public override void Tick()
        {
        }
    }
}
