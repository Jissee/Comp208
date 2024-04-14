using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class PlayerLogoutPacket : IPacket<PlayerLogoutPacket>
    {
        private string playerName;
        public PlayerLogoutPacket(string playerName)
        {
            this.playerName = playerName;
        }
        public static PlayerLogoutPacket Decode(BinaryReader reader)
        {
            return new PlayerLogoutPacket(reader.ReadString());
        }

        public static void Encode(PlayerLogoutPacket obj, BinaryWriter writer)
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
                    server.PlayerList.Kickplayer(context.PlayerSender!);
                }
            }
        }
    }
}
