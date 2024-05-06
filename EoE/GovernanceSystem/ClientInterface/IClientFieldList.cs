using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientFieldList : IFieldList
    {
        void Synchronize(FieldListRecord fieldListRecord);
        void Filedconversion(FieldStack origin, FieldStack converted);
        void Filedconversion(GameResourceType originalType, int originalcount, GameResourceType convertedType, int convertedCount);
        List<int> ToList();

    }
}
