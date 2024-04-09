using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarInvitedPacket : IPacket<WarInvitedPacket>
    {
        private string name;
        public WarInvitedPacket(string name)
        {
            this.name = name;
        }

        public static WarInvitedPacket Decode(BinaryReader reader)
        {
            string name = reader.ReadString();
            return new WarInvitedPacket(name);
        }

        public static void Encode(WarInvitedPacket obj, BinaryWriter writer)
        {
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {

            }
            else
            {

            }
        }
    }
}
