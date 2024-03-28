using EoE.GovernanceSystem;
using EoE.Server.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarTarget
    {
        public WarParty FirstParty { get; init; }
        public WarParty SecondParty { get; init; }
        private List<ResourceStack> resourceClaim;
        private List<FieldStack> fieldClaim;
        private int popClaim;
        public WarTarget(WarParty firstParty, WarParty secondParty)
        {
            this.FirstParty = firstParty;
            this.SecondParty = secondParty;
            resourceClaim = new List<ResourceStack>();
            fieldClaim = new List<FieldStack>();
        }
        public void AddResourceStack(ResourceStack newStack)
        {
            foreach (ResourceStack claim in resourceClaim)
            {
                if (claim.Type == newStack.Type)
                {
                    claim.Add(newStack);
                    return;
                }
            }
            resourceClaim.Add(newStack);
        }
        public void AddFieldStack(FieldStack newStack)
        {
            foreach (FieldStack claim in fieldClaim)
            {
                if (claim.Type == newStack.Type)
                {
                    claim.Add(newStack);
                    return;
                }
            }
            fieldClaim.Add(newStack);
        }
        public void AddPopulation(int newPop)
        {
            this.popClaim += newPop;
        }

    }
}
