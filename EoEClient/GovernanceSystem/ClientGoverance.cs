using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.GovernanceSystem
{
    public class ClientGoverance
    {
        private Dictionary<ResourcesType, int> resContain;
        private Dictionary<ResourcesType, int> popAllocation;
        private Dictionary<ResourcesType, int> secondaryResGenerateRate;
        // private Dictionary<ResourcesType, int> secondaryResConsumeRate;//Synthetic consumption
        private Dictionary<FieldsType, int> fieldContain;
        public int Population { get; private set; }
    }
}
