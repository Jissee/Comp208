using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }


}
