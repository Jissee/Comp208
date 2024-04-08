using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IClientPopulationManager:IPopulationManager
    {
        void Synchronization(PopulationRecord popRecord);
    }
}
