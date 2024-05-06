using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientResourceList : IResourceList
    {
        void Synchronize(ResourceListRecord resourceListRecord);
    }
}
