using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Treaty
{
    public interface ITreaty : ITickable
    {
        public IPlayer FirstParty { get; init; }
        public IPlayer SecondParty { get; init; }
    }
}
