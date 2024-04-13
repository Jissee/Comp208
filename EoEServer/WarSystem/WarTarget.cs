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
        public List<ResourceStack> ResourceClaim { get; private set; }
        public int FieldClaim { get; private set; }
        public int PopClaim { get; private set; }
        public WarTarget(IWarParty firstParty, IWarParty secondParty)
        {
            this.FirstParty = firstParty;
            this.SecondParty = secondParty;
            ResourceClaim = new List<ResourceStack>();
        }
        public void AddResourceStack(ResourceStack newStack)
        {
            foreach (ResourceStack claim in ResourceClaim)
            {
                if (claim.Type == newStack.Type)
                {
                    claim.Add(newStack);
                    return;
                }
            }
            ResourceClaim.Add(newStack);
        }
        public void AddFieldCount(int newField)
        {
            FieldClaim += newField;
        }
        public void AddPopulation(int newPop)
        {
            this.PopClaim += newPop;
        }

    }
}
