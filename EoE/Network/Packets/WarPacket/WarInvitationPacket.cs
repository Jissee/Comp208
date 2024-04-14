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
        private string[] names;
        public WarInvitationPacket(string warName, string[] names)
        {
            this.warName = warName;
            this.names = names;
        }
        public static WarInvitationPacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarInvitationPacket(warName, names);
        }

        public static void Encode(WarInvitationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.names.Length);
            for (int i = 0; i < obj.names.Length; i++)
            {
                writer.Write(obj.names[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver!;
                IPlayer player = context.PlayerSender!;
                server.Broadcast(new WarInvitedPacket(warName, false, player.PlayerName) , (invitedPlayer)=>names.Contains(invitedPlayer.PlayerName));
            }
            else
            {
                // todo: response whether join the war
            }
        }
    }
}
