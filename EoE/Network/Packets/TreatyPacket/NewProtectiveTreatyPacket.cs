using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.TreatyPacket
{
    public class NewProtectiveTreatyPacket : IPacket<NewProtectiveTreatyPacket>
    {
        private ResourceListRecord resourceListRecord;
        private bool requestForProtection;
        private string sender;
        private string receiver;
        public NewProtectiveTreatyPacket(ResourceListRecord resourceListRecord,bool requestForProtection, string sender, string receiver)
        {
            this.resourceListRecord = resourceListRecord;
            this.requestForProtection = requestForProtection;
            this.sender = sender;
            this.receiver = receiver;
        }

        public static NewProtectiveTreatyPacket Decode(BinaryReader reader)
        {
            return new NewProtectiveTreatyPacket(
                ResourceListRecord.decoder(reader),
                reader.ReadBoolean(),
                reader.ReadString(),
                reader.ReadString()
                );
        }

        public static void Encode(NewProtectiveTreatyPacket obj, BinaryWriter writer)
        {
            ResourceListRecord.encoder(obj.resourceListRecord, writer);
            writer.Write(obj.requestForProtection);
            writer.Write(obj.sender);
            writer.Write(obj.receiver);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer senderPlayer = context.PlayerSender!;
                IPlayer receiverPlayer = server.GetPlayer(receiver)!;
                NewProtectiveTreatyPacket packet = new NewProtectiveTreatyPacket(
                    resourceListRecord,
                    requestForProtection,
                    sender,
                    receiver
                    );
                receiverPlayer.SendPacket( packet );
            }
            else
            {

            }
        }
    }
}
