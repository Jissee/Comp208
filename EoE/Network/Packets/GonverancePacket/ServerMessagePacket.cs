using EoE.Network.Entities;
using System.Diagnostics;

namespace EoE.Network.Packets.GonverancePacket
{
    public class ServerMessagePacket : IPacket<ServerMessagePacket>
    {
        public static string SERVER_FULL = "Server is full! The program will exit and you have to wait until the end of the current game.";
        private string message;

        public ServerMessagePacket(string message)
        {
            this.message = message;
        }
        public static ServerMessagePacket Decode(BinaryReader reader)
        {
            return new ServerMessagePacket(reader.ReadString());
        }

        public static void Encode(ServerMessagePacket obj, BinaryWriter writer)
        {
            writer.Write(obj.message);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    if (message == SERVER_FULL)
                    {
                        client.MsgBoxYesNo(message);
                        Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        client.MsgBox(message);
                    }

                }
            }
        }
    }
}
