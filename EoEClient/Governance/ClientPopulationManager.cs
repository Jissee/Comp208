using EoE.Governance;
using EoE.Governance.ClientInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Client.Governance
{
    public class ClientPopulationManager : IClientPopManager
    {
        private Dictionary<GameResourceType, int> popAloc = new Dictionary<GameResourceType, int>();

        public int ExploratoinPopulation { get; private set; }
        public int AvailablePopulation { get; set; }

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
            popAloc[GameResourceType.Industrial] = popRecord.industrialPop;
            AvailablePopulation = popRecord.availablePopulation;
            WindowManager.INSTANCE.UpdatePopulation();
        }

        public void SetAllocation(PopulationRecord record)
        {
            List<int> list = [record.siliconPop, record.copperPop, record.ironPop, record.aluminumPop, record.electronicPop, record.industrialPop];
            if (list.Min() < 0)
            {
                Client.INSTANCE.MsgBox("Negative input");
            }
            int count = record.siliconPop + record.copperPop + record.ironPop + record.aluminumPop + record.electronicPop + record.industrialPop;
            if (TotalPopulation >= count)
            {
                Client.INSTANCE.SendPacket(new SetPopAllocationPacket(record));
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

        public PopulationRecord GetPopulationRecord()
        {
            return new PopulationRecord(
                popAloc[GameResourceType.Silicon],
                popAloc[GameResourceType.Copper],
                popAloc[GameResourceType.Iron],
                popAloc[GameResourceType.Aluminum],
                popAloc[GameResourceType.Electronic],
                popAloc[GameResourceType.Industrial],
                AvailablePopulation
                );
        }

    }
}
