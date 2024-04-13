using EoE.GovernanceSystem;
using EoE.Server.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarTarget : IWarTarget
    {
        public IWarParty FirstParty { get; init; }
        public IWarParty SecondParty { get; init; }
        private List<ResourceStack> resourceClaim;
        private int fieldClaim;
        private int popClaim;
        public WarTarget(IWarParty firstParty, IWarParty secondParty)
        {
            this.FirstParty = firstParty;
            this.SecondParty = secondParty;
            resourceClaim = new List<ResourceStack>();
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
        public void AddFieldCount(int newField)
        {
            fieldClaim += newField;
        }
        public void AddPopulation(int newPop)
        {
            this.popClaim += newPop;
        }

    }
}
