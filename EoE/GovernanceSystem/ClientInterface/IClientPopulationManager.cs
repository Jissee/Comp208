using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientPopulationManager : IPopManager
    {
        void Synchronize(PopulationRecord popRecord);
        public void SetAllocation(
           int siliconPop,
           int copperPop,
           int ironPop,
           int aluminumPop,
           int electronicPop,
           int industrailPop
           );
    }
}
