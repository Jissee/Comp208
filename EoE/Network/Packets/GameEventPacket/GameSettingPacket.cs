﻿using EoE.Network.Entities;

namespace EoE.Network.Packets.GameEventPacket
{
    public class GameSettingPacket : IPacket<GameSettingPacket>
    {
        private GameSettingRecord record;

        public GameSettingPacket(GameSettingRecord record)
        {
            this.record = record;
        }

        public static GameSettingPacket Decode(BinaryReader reader)
        {
            return new GameSettingPacket(GameSettingRecord.decoder(reader));
        }

        public static void Encode(GameSettingPacket obj, BinaryWriter writer)
        {
            GameSettingRecord.encoder(obj.record, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    server.SetGame(record.playerCount, record.TotalTick);
                    server.Boardcast(new GameSettingPacket(new GameSettingRecord(record.playerCount, record.TotalTick)), player => true);
                }
            }
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.WindowManager.UpdateGameSetting(record.playerCount, record.TotalTick);
                }
            }
        }
    }
}
