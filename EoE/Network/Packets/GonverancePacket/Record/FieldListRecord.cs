﻿namespace EoE.Network.Packets.GonverancePacket.Record
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
            this.siliconFieldCount = silicon;
            this.copperFieldCount = copper;
            this.ironFieldCount = iron;
            this.aluminumFieldCount = aluminum;
            this.electronicFieldCount = electronic;
            this.industrialFieldCount = industrial;

        }
        public FieldListRecord() : this(0, 0, 0, 0, 0, 0)
        {

        }
        public List<int> ToList()
        {
            List<int> list =
                [this.siliconFieldCount,
                this.copperFieldCount,
                this.ironFieldCount,
                this.aluminumFieldCount,
                this.electronicFieldCount,
                this.industrialFieldCount,
            ];
            return list;
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
            reader.ReadInt32(),
            reader.ReadInt32(),
            reader.ReadInt32(),
            reader.ReadInt32(),
            reader.ReadInt32(),
            reader.ReadInt32()
                );
        };
    }
}
