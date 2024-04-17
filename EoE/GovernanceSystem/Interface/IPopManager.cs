using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket.Record;
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
        PopulationRecord GetPopulationRecord();

    }
}
