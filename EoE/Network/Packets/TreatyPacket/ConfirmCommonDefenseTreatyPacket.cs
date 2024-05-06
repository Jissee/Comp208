using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Treaty;

namespace EoE.Network.Packets.TreatyPacket
{
    public class ConfirmCommonDefenseTreatyPacket : IPacket<ConfirmCommonDefenseTreatyPacket>
    {
        private string sender;
        private string receiver;
        private bool accepted;
        public ConfirmCommonDefenseTreatyPacket(string sender, string receiver, bool accepted)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.accepted = accepted;
        }

        public static ConfirmCommonDefenseTreatyPacket Decode(BinaryReader reader)
        {
            return new ConfirmCommonDefenseTreatyPacket(
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadBoolean()
                );
        }

        public static void Encode(ConfirmCommonDefenseTreatyPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.sender);
            writer.Write(obj.receiver);
            writer.Write(obj.accepted);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer senderPlayer = server.GetPlayer(sender)!;
                IPlayer receiverPlayer = context.PlayerSender!;
                ITreatyManager manager = server.PlayerList.TreatyManager;
                if (accepted == true)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("The player accepted your request!");
                    senderPlayer.SendPacket(packet);
                    manager.AddCommonDefenseTreaty(senderPlayer, receiverPlayer);
                }
                else
                {
                    ServerMessagePacket packet = new ServerMessagePacket("The player rejected your request!");
                    senderPlayer.SendPacket(packet);
                }
            }
        }
    }
}
