using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket.Record
{
    public struct FieldListRecord
    {
        public int siliconFieldCount;
        public int copperFieldCount;
        public int ironFieldCount;
        public int aluminumFieldCount;
        public int electronicFieldCount;
        public int industrialFieldCount;

        public FieldListRecord(
            int silicon,
            int copper,
            int iron,
            int aluminum,
            int electronic,
            int industrial)
        {
            int siliconFieldCount = silicon;
            int copperFieldCount = copper;
            int ironFieldCount = iron;
            int aluminumFieldCount= aluminum;
            int electronicFieldCount= electronic;
            int industrialFieldCount= industrial;

        }
        public FieldListRecord() : this(0, 0, 0, 0, 0, 0)
        {

        }

        public static Encoder<FieldListRecord> encoder = (FieldListRecord obj, BinaryWriter writer) =>
        {
            writer.Write(obj.siliconFieldCount);
            writer.Write(obj.copperFieldCount);
            writer.Write(obj.ironFieldCount);
            writer.Write(obj.aluminumFieldCount);
            writer.Write(obj.electronicFieldCount);
            writer.Write(obj.industrialFieldCount);
        };

        public static Decoder<FieldListRecord> decoder = (BinaryReader reader) =>
        {
            return new FieldListRecord(
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count,
            ResourceStack.decoder(reader).Count
                );
        };
    }
}
