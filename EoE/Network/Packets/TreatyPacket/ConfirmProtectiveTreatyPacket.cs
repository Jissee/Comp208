using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Treaty;

namespace EoE.Network.Packets.TreatyPacket
{
    public class ConfirmProtectiveTreatyPacket : IPacket<ConfirmProtectiveTreatyPacket>
    {
        private ResourceListRecord resourceListRecord;
        private bool requestForProtection;
        private string sender;
        private string receiver;
        private bool accepted;
        public ConfirmProtectiveTreatyPacket(ResourceListRecord resourceListRecord, bool requestForProtection, string sender, string receiver, bool accepted)
        {
            this.resourceListRecord = resourceListRecord;
            this.requestForProtection = requestForProtection;
            this.sender = sender;
            this.receiver = receiver;
            this.accepted = accepted;
        }

        public static ConfirmProtectiveTreatyPacket Decode(BinaryReader reader)
        {
            return new ConfirmProtectiveTreatyPacket(
                ResourceListRecord.decoder(reader),
                reader.ReadBoolean(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadBoolean()
                );
        }

        public static void Encode(ConfirmProtectiveTreatyPacket obj, BinaryWriter writer)
        {
            ResourceListRecord.encoder(obj.resourceListRecord, writer);
            writer.Write(obj.requestForProtection);
            writer.Write(obj.sender);
            writer.Write(obj.receiver);
            writer.Write(obj.accepted);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer senderPlayer = server.GetPlayer(sender)!;
                IPlayer receiverPlayer = context.PlayerSender!;
                ITreatyManager manager = server.PlayerList.TreatyManager;
                if (accepted == true)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("The player accepted your request!");
                    senderPlayer.SendPacket(packet);
                    if (requestForProtection)
                    {
                        manager.AddProtectiveTreaty(senderPlayer, receiverPlayer, resourceListRecord);
                    }
                    else
                    {
                        manager.AddProtectiveTreaty(receiverPlayer, senderPlayer, resourceListRecord);
                    }
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
