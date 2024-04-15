using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarInvitationPacket : IPacket<WarInvitationPacket>
    {
        private string warName;
        private string name;
        public WarInvitationPacket(string warName, string name)
        {
            this.warName = warName;
            this.name = name;
        }
        public static WarInvitationPacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            string name = reader.ReadString();
            return new WarInvitationPacket(warName, name);
        }

        public static void Encode(WarInvitationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.name);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver!;
                IPlayer player = context.PlayerSender!;
                IPlayer target = server.GetPlayer(name)!;
                target.SendPacket(new WarInvitedPacket(warName, false, player.PlayerName));
            }
            else
            {
                // todo: response whether join the war
            }
        }
    }
}
