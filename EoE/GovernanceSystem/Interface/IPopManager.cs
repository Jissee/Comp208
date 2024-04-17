using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IPopManager
    {
        int TotalPopulation { get; }
        int AvailablePopulation { get;  }
        int GetPopAllocCount(GameResourceType type);

    }
}
