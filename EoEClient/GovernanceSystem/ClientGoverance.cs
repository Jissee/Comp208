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
        private Dictionary<GameResourceType, int> resContain;
        private Dictionary<GameResourceType, int> popAllocation;
        private Dictionary<GameResourceType, int> secondaryResGenerateRate;
        // private Dictionary<ResourcesType, int> secondaryResConsumeRate;//Synthetic consumption
        private Dictionary<GameResourceType, int> fieldContain;
        public int Population { get; private set; }
    }
}
