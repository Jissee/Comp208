using EoE.Network.Packets;

namespace EoE.Server.WarSystem
{
    public struct WarTarget
    {
        public int SiliconClaim { get; set; }
        public int CopperClaim { get; set; }
        public int IronClaim { get; set; }
        public int AluminumClaim { get; set; }
        public int ElectronicClaim { get; set; }
        public int IndustrialClaim { get; set; }
        public int FieldClaim { get; set; }
        public int PopClaim { get; set; }
        public void Add(WarTarget target)
        {
            SiliconClaim += target.SiliconClaim;
            CopperClaim += target.CopperClaim;
            IronClaim += target.IronClaim;
            AluminumClaim += target.AluminumClaim;
            ElectronicClaim += target.ElectronicClaim;
            IndustrialClaim += target.IndustrialClaim;
            FieldClaim += target.FieldClaim;
            PopClaim += target.PopClaim;
        }
        public static Encoder<WarTarget> encoder = (obj, writer) =>
        {
            writer.Write(obj.SiliconClaim);
            writer.Write(obj.CopperClaim);
            writer.Write(obj.IronClaim);
            writer.Write(obj.AluminumClaim);
            writer.Write(obj.ElectronicClaim);
            writer.Write(obj.IndustrialClaim);
            writer.Write(obj.FieldClaim);
            writer.Write(obj.PopClaim);
        };
        public static Decoder<WarTarget> decoder = (reader) =>
        {
            WarTarget obj = new WarTarget();
            obj.SiliconClaim = reader.ReadInt32();
            obj.CopperClaim = reader.ReadInt32();
            obj.IronClaim = reader.ReadInt32();
            obj.AluminumClaim = reader.ReadInt32();
            obj.ElectronicClaim = reader.ReadInt32();
            obj.IndustrialClaim = reader.ReadInt32();
            obj.FieldClaim = reader.ReadInt32();
            obj.PopClaim = reader.ReadInt32();
            return obj;
        };
    }
}
