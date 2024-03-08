using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    public class NewPacket : IPacket<NewPacket>
    {
        private int x;
        private double y;
        public NewPacket(int x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static NewPacket Decode(BinaryReader reader)
        {
            int x = reader.ReadInt32();
            double y = reader.ReadDouble();
            return new NewPacket(x, y);
        }

        public static void Encode(NewPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.x);
            writer.Write(obj.y);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Server2Client)
            {
                IClient client = (IClient)context.Receiver;
                client.MsgBox($"{client.PlayerName} received from {context.PlayerSender.PlayerName}, x = {x}, y = {y}");
            }
            else
            {
                IPlayer player = context.PlayerSender!;
                IServer server = (IServer)context.Receiver;
                server.Broadcast(this, (player) => true);

            }
            
        }
    }
}
