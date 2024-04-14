using EoE.GovernanceSystem;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using EoE.Network.Packets.GonverancePacket;
using EoE.GovernanceSystem.ClientInterface;

namespace EoE.Client.GovernanceSystem
{
    public class ClientPopulationManager: IClientPopulationManager
    {
        private Dictionary<GameResourceType, int> popAloc;

        public int ExploratoinPopulation { get; private set; }
        public int AvailablePopulation { get; private set; }

        public int TotalPopulation
        {
            get
            {
                int count = 0;
                foreach (var kvp in popAloc)
                {
                    count += kvp.Value;
                }

                count += AvailablePopulation;
                return count;
            }
        }

        public ClientPopulationManager()
        {
            popAloc.Add(GameResourceType.Silicon, 0);
            popAloc.Add(GameResourceType.Copper, 0);
            popAloc.Add(GameResourceType.Iron, 0);
            popAloc.Add(GameResourceType.Aluminum, 0);
            popAloc.Add(GameResourceType.Industrial, 0);
            popAloc.Add(GameResourceType.Electronic, 0);
            AvailablePopulation = 0;
        }
        public void Synchronize(PopulationRecord popRecord)
        {
            popAloc[GameResourceType.Silicon] = popRecord.siliconPop;
            popAloc[GameResourceType.Copper] = popRecord.copperPop;
            popAloc[GameResourceType.Iron] = popRecord.ironPop;
            popAloc[GameResourceType.Aluminum] = popRecord.aluminumPop;
            popAloc[GameResourceType.Electronic] = popRecord.electronicPop;
            popAloc[GameResourceType.Industrial] = popRecord.industrailPop;
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
                popAloc[GameResourceType.Silicon] = siliconPop;
                popAloc[GameResourceType.Copper] = copperPop;
                popAloc[GameResourceType.Iron] = ironPop;
                popAloc[GameResourceType.Aluminum] = aluminumPop;
                popAloc[GameResourceType.Electronic] = electronicPop;
                popAloc[GameResourceType.Industrial] = industrailPop;
                AvailablePopulation = TotalPopulation - count;
                Client.INSTANCE.SendPacket(new SetPopAllocationPacket(
                    new PopulationRecord(
                    siliconPop,
                    copperPop,
                    ironPop,
                    aluminumPop,
                    electronicPop,
                    industrailPop,
                    AvailablePopulation)
                    ));
            }
            else
            {
                Client.INSTANCE.MsgBox("Population insufficient");
            }
        }

        public int GetPopAllocCount(GameResourceType type)
        {
            return popAloc[type];
        }
    }
}
