using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using EoE.Network.Packets.GonverancePacket;

namespace EoE.Client.GovernanceSystem
{
    public class ClientPopulationManager: IClientPopulationManager
    {
        public int SiliconPop { get; private set; }
        public int CopperPop { get; private set; }
        public int IronPop { get; private set; }
        public int AluminumPop { get; private set; }
        public int ElectronicPop { get; private set; }
        public int IndustrailPop { get; private set; }

        public int AvailablePopulation { get; private set; }

        public int TotalPopulation => SiliconPop + CopperPop +
            IronPop + AluminumPop + ElectronicPop +
            IndustrailPop + AvailablePopulation;

        public ClientPopulationManager()
        {
            SiliconPop = 0;
            CopperPop = 0;
            IronPop = 0;
            AluminumPop = 0;
            ElectronicPop = 0;
            IndustrailPop = 0;
            AvailablePopulation = 0;
        }
        public void Synchronize(PopulationRecord popRecord)
        {
            SiliconPop = popRecord.siliconPop;
            CopperPop = popRecord.copperPop;
            IronPop = popRecord.ironPop;
            AluminumPop = popRecord.aluminumPop;
            ElectronicPop = popRecord.electronicPop;
            IndustrailPop = popRecord.industrailPop;
            AvailablePopulation = popRecord.availablePopulation;
        }

        public void SetAllocation(
            int siliconPop,
            int copperPop,
            int ironPop,
            int aluminumPop,
            int electronicPop,
            int industrailPop
            )
        {
            List<int> list = [siliconPop, copperPop, ironPop, aluminumPop, electronicPop, industrailPop];
            if (list.Min() < 0)
            {
                Client.INSTANCE.MsgBox("Negative input");
            }
            int count = siliconPop + copperPop + ironPop + aluminumPop + electronicPop + industrailPop;
            if (TotalPopulation >= count)
            {
                ResetPopAllocation();
                SiliconPop = siliconPop;
                CopperPop = copperPop;
                IronPop = ironPop;
                AluminumPop = aluminumPop;
                ElectronicPop = electronicPop;
                IndustrailPop = industrailPop;
                AvailablePopulation = TotalPopulation - count;
                Client.INSTANCE.SendPacket(new SetPopAllocationPacket(
                    siliconPop, 
                    copperPop, 
                    ironPop, 
                    aluminumPop,
                    electronicPop, 
                    industrailPop
                    ));
            }
            else
            {
                Client.INSTANCE.MsgBox("Population insufficient");
            }
        }

        private void ResetPopAllocation()
        {
            SiliconPop = 0;
            CopperPop = 0;
            IronPop = 0;
            AluminumPop = 0;
            ElectronicPop = 0;
            IndustrailPop = 0;
        }
    }
}
