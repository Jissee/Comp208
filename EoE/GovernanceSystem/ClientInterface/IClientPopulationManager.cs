using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientPopulationManager : IPopManager
    {
        void Synchronize(PopulationRecord popRecord);
    }
}
