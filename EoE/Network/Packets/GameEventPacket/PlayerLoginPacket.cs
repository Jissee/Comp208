using EoE.Network.Entities;

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
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.SynchronizePlayerName(playerName);
                }
            }
        }
    }
}
