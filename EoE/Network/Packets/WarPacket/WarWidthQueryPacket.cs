using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarWidthQueryPacket : IPacket<WarWidthQueryPacket>
    {
        private string warName;
        private int width;
        public WarWidthQueryPacket(string warName, int width)
        {
            this.warName = warName;
            this.width = width;
        }

        public static WarWidthQueryPacket Decode(BinaryReader reader)
        {
            return new WarWidthQueryPacket(reader.ReadString(), reader.ReadInt32());
        }

        public static void Encode(WarWidthQueryPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.width);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                WarWidthQueryPacket packet = new WarWidthQueryPacket(warName, width);
                player.SendPacket(packet);
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.ClientWarWidthList.ChangeWarWidth(warName, width);
            }
        }
    }
}
