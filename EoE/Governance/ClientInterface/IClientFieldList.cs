using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    public interface IClientFieldList : IFieldList
    {
        List<int> ToList();
        void Synchronize(FieldListRecord fieldListRecord);
    }
}
