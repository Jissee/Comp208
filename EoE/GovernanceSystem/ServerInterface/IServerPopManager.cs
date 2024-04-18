using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerPopManager : IPopManager
    {
        int ExploratoinPopulation { get; }
        void SetAllocation(int siliconPop, int copperPop, int ironPop, int aluminumPop, int electronic, int industrialPop);
        void AlterPop(int count);
        void SetExploration(int population);
        void ClearAll();
    }
}
