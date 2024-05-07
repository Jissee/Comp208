using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.Interface
{
    public interface IPopManager
    {
        int TotalPopulation { get; }
        int AvailablePopulation { get; }
        int GetPopAllocCount(GameResourceType type);
        //void SetAllocation(int siliconPop, int copperPop, int ironPop, int aluminumPop, int electronic, int industrialPop);
        void SetAllocation(PopulationRecord record);

        PopulationRecord GetPopulationRecord();

    }
}
