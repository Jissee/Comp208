using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket
{
    public class FieldBoardCastPacket : IPacket<FieldBoardCastPacket>
    {
        private FieldListRecord playerFieldList;
        public FieldBoardCastPacket(FieldListRecord playerFieldList)
        {
            this.playerFieldList = playerFieldList;
        }
        public static FieldBoardCastPacket Decode(BinaryReader reader)
        {
            return new FieldBoardCastPacket(FieldListRecord.decoder(reader));
        }

        public static void Encode(FieldBoardCastPacket obj, BinaryWriter writer)
        {
            FieldListRecord.encoder(obj.playerFieldList, writer);
        }

        public void Handle(PacketContext context)
        {
            //todo
        }
    }
}
