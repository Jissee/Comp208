﻿using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public abstract class RelationTreaty : Treaty, ITickable
    {
        public Dictionary<GameResourceType, int> ConditionEntries { get; init; }

        public RelationTreaty(ServerPlayer firstParty, ServerPlayer secondParty) : base(firstParty, secondParty) 
        { 
            ConditionEntries = new Dictionary<GameResourceType, int>();
        }
        public void AddCondition(ResourceStack resourceStack)
        {
            if (!ConditionEntries.ContainsKey(resourceStack.Type))
            {
                ConditionEntries[resourceStack.Type] = resourceStack.Count;
            }
            else
            {
                ConditionEntries[resourceStack.Type] += resourceStack.Count;
            }
        }
        public abstract void Tick();
    }
}
