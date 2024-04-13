using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class CommonDefenseTreaty : RelationTreaty
    {
        public CommonDefenseTreaty(IPlayer firstParty, IPlayer secondParty) : base(firstParty, secondParty)
        {
        }

        public override void Tick()
        {
        }
    }
}
