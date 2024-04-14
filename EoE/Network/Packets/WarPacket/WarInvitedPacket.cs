using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarInvitedPacket : IPacket<WarInvitedPacket>
    {
        private string warName;
        private bool accepted;
        private string name;
        public WarInvitedPacket(string warName, bool accepted, string name)
        {
            this.warName = warName;
            this.accepted = accepted;
            this.name = name;
        }

        public static WarInvitedPacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            bool accepted = reader.ReadBoolean();
            string name = reader.ReadString();
            
            return new WarInvitedPacket(warName, accepted, name);
        }

        public static void Encode(WarInvitedPacket obj, BinaryWriter writer)
        {
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                //
            }
            else
            {
                //todo player choose to accept or not;
            }
        }
    }
}
