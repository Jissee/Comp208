namespace EoE.Network.Packets.GonverancePacket.Record
{
    public struct PopulationRecord
    {
        public int siliconPop;
        public int copperPop;
        public int ironPop;
        public int aluminumPop;
        public int electronicPop;
        public int industrailPop;
        public int availablePopulation;

        public PopulationRecord(
            int siliconPop,
            int copperPop,
            int ironPop,
            int aluminumPop,
            int electronicPop,
            int industrailPop,
            int availablePopulation
            )
        {
            this.siliconPop = siliconPop;
            this.copperPop = copperPop;
            this.ironPop = ironPop;
            this.aluminumPop = aluminumPop;
            this.electronicPop = electronicPop;
            this.industrailPop = industrailPop;
            this.availablePopulation = availablePopulation;
        }

        public static Encoder<PopulationRecord> encoder = (PopulationRecord obj, BinaryWriter writer) =>
        {
            writer.Write(obj.siliconPop);
            writer.Write(obj.copperPop);
            writer.Write(obj.ironPop);
            writer.Write(obj.aluminumPop);
            writer.Write(obj.electronicPop);
            writer.Write(obj.industrailPop);
            writer.Write(obj.availablePopulation);
        };

        public static Decoder<PopulationRecord> decoder = (BinaryReader reader) =>
        {
            return new PopulationRecord(
                reader.ReadInt32(),
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
