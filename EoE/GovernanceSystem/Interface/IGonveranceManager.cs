using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IGonveranceManager : ITickable
    {
        public static readonly int EXPLORE_RESOURCE_PER_POP = 5;
       
    }
    
}
