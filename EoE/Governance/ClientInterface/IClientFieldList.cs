using EoE.Governance.Interface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.ClientInterface
{
    /// <summary>
    /// The client side interface of field list.
    /// </summary>
    public interface IClientFieldList : IFieldList
    {
        List<int> ToList();
        void Synchronize(FieldListRecord fieldListRecord);
    }
}
