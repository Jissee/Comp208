using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public abstract class Treaty : ITickable
    {
        public ServerPlayer FirstParty { get; init; }
        public ServerPlayer SecondParty { get; init; }
        public Dictionary<GameResourceType, int> TradeEntries { get; init; }

        public Treaty(ServerPlayer firstParty, ServerPlayer secondParty) 
        { 
            this.FirstParty = firstParty;
            this.SecondParty = secondParty;
            TradeEntries = new Dictionary<GameResourceType, int>();
        }
        public void AddCondition(ResourceStack resourceStack)
        {
            if (!TradeEntries.ContainsKey(resourceStack.Type))
            {
                TradeEntries[resourceStack.Type] = resourceStack.Count;
            }
            else
            {
                TradeEntries[resourceStack.Type] += resourceStack.Count;
            }
        }
        public abstract void Tick();
    }
}
