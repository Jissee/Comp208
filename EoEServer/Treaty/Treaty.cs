using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class Treaty : ITreaty
    {
        public IPlayer FirstParty { get; init; }
        public IPlayer SecondParty { get; init; }

        public Treaty(IPlayer firstParty, IPlayer secondParty)
        {
            FirstParty = firstParty;
            SecondParty = secondParty;
        }

        public virtual void Tick() { }
    }
}
