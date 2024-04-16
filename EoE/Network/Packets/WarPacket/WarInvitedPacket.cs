using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.WarSystem.Interface;
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
        private string invitorName;
        private string accepterName;
        public WarInvitedPacket(string warName, bool accepted, string invitorName, string accepterName)
        {
            this.warName = warName;
            this.accepted = accepted;
            this.invitorName = invitorName;
            this.accepterName = accepterName;
        }

        public static WarInvitedPacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            bool accepted = reader.ReadBoolean();
            string iname = reader.ReadString();
            string aname = reader.ReadString();
            return new WarInvitedPacket(warName, accepted, iname, aname);
        }

        public static void Encode(WarInvitedPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.accepted);
            writer.Write(obj.invitorName);
            writer.Write(obj.accepterName);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                if (accepted == true)
                {
                    IServer server = (IServer)context.Receiver;
                    IPlayer player = context.PlayerSender!;
                    IWarManager warManager = server.PlayerList.WarManager;
                    if (warManager.PreparingWarDict.ContainsKey(warName))
                    {
                        warManager.PreparingWarDict[warName].Attackers.AddPlayer(player);
                    }
                    ServerMessagePacket packet = new ServerMessagePacket(accepterName + " accepted your invitation!");
                    IPlayer invitor = server.GetPlayer(invitorName)!;
                    invitor.SendPacket(packet);
                }
                else
                {
                    IServer server = (IServer)context.Receiver;
                    ServerMessagePacket packet = new ServerMessagePacket(accepterName + " rejected your invitation!");
                    IPlayer invitor = server.GetPlayer(invitorName)!;
                    invitor.SendPacket(packet);
                }
            }
        }
    }
}
