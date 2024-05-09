using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    /// <summary>
    /// The client side interface of popuation manager.
    /// </summary>
    public interface IClientPopManager : IPopManager
    {
        void Synchronize(PopulationRecord popRecord);
    }
}
