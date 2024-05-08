using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    public interface IClientResourceList : IResourceList
    {
        void Synchronize(ResourceListRecord resourceListRecord);
    }
}
