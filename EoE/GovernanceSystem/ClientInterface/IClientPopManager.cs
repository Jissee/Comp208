using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientPopManager : IPopManager
    {
        void Synchronize(PopulationRecord popRecord);
    }
}
