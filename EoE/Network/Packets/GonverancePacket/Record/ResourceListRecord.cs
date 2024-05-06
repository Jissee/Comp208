using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;

namespace EoE.Network.Packets.GonverancePacket.Record
{
    public struct ResourceListRecord
    {
        public int siliconCount;
        public int copperCount;
        public int ironCount;
        public int aluminumCount;
        public int electronicCount;
        public int industrialCount;
        public int battleArmyCount;
        public int informativeArmyCount;
        public int mechanismArmyCount;
        public ResourceListRecord
            (
            int silicon,
            int coppor,
            int iron,
            int aluminum,
            int electronic,
            int industrial,
            int battleArmy,
            int informativeArmy,
            int mechanismArmy
            )
        {
            this.siliconCount = silicon;
            this.copperCount = coppor;
            this.ironCount = iron;
            this.aluminumCount = aluminum;
            this.electronicCount = electronic;
            this.industrialCount = industrial;
            this.battleArmyCount = battleArmy;
            this.informativeArmyCount = informativeArmy;
            this.mechanismArmyCount = mechanismArmy;

        }
        public ResourceListRecord() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public static Encoder<ResourceListRecord> encoder = (ResourceListRecord obj, BinaryWriter writer) =>
        {
            writer.Write(obj.siliconCount);
            writer.Write(obj.copperCount);
            writer.Write(obj.ironCount);
            writer.Write(obj.aluminumCount);
            writer.Write(obj.electronicCount);
            writer.Write(obj.industrialCount);
            writer.Write(obj.battleArmyCount);
            writer.Write(obj.informativeArmyCount);
            writer.Write(obj.mechanismArmyCount);
        };

        public static Decoder<ResourceListRecord> decoder = (BinaryReader reader) =>
        {
            return new ResourceListRecord(
            reader.ReadInt32(),
            reader.ReadInt32(),
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
