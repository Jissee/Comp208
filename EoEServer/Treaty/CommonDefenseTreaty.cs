using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class CommonDefenseTreaty : RelationTreaty
    {
        public CommonDefenseTreaty(ServerPlayer firstParty, ServerPlayer secondParty) : base(firstParty, secondParty)
        {
        }

        public override void Tick()
        {
        }
    }
}
