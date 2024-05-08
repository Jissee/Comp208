using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.Interface
{
    public interface IPopManager
    {
        int TotalPopulation { get; }
        int AvailablePopulation { get; }
        int GetPopAllocCount(GameResourceType type);
        void SetAllocation(PopulationRecord record);

        PopulationRecord GetPopulationRecord();

    }
}
