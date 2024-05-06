using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.Interface
{
    public interface IFieldList
    {
        int TotalFieldCount { get; }
        void AddFieldStack(FieldStack adder);
        void AddField(GameResourceType type, int count);
        FieldStack SplitFieldStack(FieldStack field);
        FieldStack SplitField(GameResourceType type, int count);
        int GetFieldCount(GameResourceType type);
        FieldStack GetFieldStack(GameResourceType type);
        FieldListRecord GetFieldListRecord();

    }


}
