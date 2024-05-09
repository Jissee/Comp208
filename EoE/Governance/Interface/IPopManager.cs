using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.Interface
{
    /// <summary>
    /// The base interface of population manager, including pupulation growth and field allocation.
    /// </summary>
    public interface IPopManager
    {
        int TotalPopulation { get; }
        int AvailablePopulation { get; }
        int GetPopAllocCount(GameResourceType type);
        void SetAllocation(PopulationRecord record);

        PopulationRecord GetPopulationRecord();

    }
}
