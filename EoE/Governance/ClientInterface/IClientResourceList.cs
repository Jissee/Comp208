using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    /// <summary>
    /// The client side interface of resource list.
    /// </summary>
    public interface IClientResourceList : IResourceList
    {
        void Synchronize(ResourceListRecord resourceListRecord);
    }
}
