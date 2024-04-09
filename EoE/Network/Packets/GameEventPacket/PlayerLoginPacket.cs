using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class PlayerLoginPacket : IPacket<PlayerLoginPacket>
    {
        private string playerName;

        public PlayerLoginPacket(string playerName)
        {
            this.playerName = playerName;
        }
        public static PlayerLoginPacket Decode(BinaryReader reader)
        {
            return new PlayerLoginPacket(reader.ReadString());
        }

        public static void Encode(PlayerLoginPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.playerName);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    server.InitPlayerName(context.PlayerSender!, playerName);
                }
            }
        }
    }
}
