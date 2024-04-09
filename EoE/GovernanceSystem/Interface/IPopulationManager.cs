using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IPopulationManager
    {
        public int SiliconPop { get; }
        public int CopperPop { get; }
        public int IronPop { get; }
        public int AluminumPop { get; }
        public int ElectronicPop { get; }
        public int IndustrailPop { get; }

        public int AvailablePopulation { get; }

        public int TotalPopulation { get; }
        

        void ResetPopAllocation();
    }
}
