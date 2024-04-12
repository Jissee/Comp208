using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket
{
    public class ServerMessagePacket : IPacket<ServerMessagePacket>
    {
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
                    client.MsgBox(message);
                }
            }
        }
    }
}
