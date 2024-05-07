using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientFieldList : IFieldList
    {
        List<int> ToList();
        void Synchronize(FieldListRecord fieldListRecord);
    }
}
