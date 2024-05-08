using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    public interface IClientPopManager : IPopManager
    {
        void Synchronize(PopulationRecord popRecord);
    }
}
