using EoE.GovernanceSystem;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public struct GameSettingRecord
    {
        public int playerCount;
        public int TotalTick;

        public GameSettingRecord(int playerCount, int TotalTick)
        {
            this.playerCount = playerCount;
            this.TotalTick = TotalTick;
        }
        public static Encoder<GameSettingRecord> encoder = (GameSettingRecord obj, BinaryWriter writer) =>
        {
            writer.Write(obj.playerCount);
            writer.Write(obj.TotalTick);
        };

        public static Decoder<GameSettingRecord> decoder = (BinaryReader reader) =>
        {
            return new GameSettingRecord(
            reader.ReadInt32(),
            reader.ReadInt32()
                ) ;
        };
    }
}
