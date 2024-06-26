﻿using EoE.Network.Entities;

namespace EoE.Network.Packets.TreatyPacket
{
    public class NewCommonDefenseTreatyPacket : IPacket<NewCommonDefenseTreatyPacket>
    {
        private string sender;
        private string receiver;
        public NewCommonDefenseTreatyPacket(string sender, string receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public static NewCommonDefenseTreatyPacket Decode(BinaryReader reader)
        {
            return new NewCommonDefenseTreatyPacket(
                reader.ReadString(),
                reader.ReadString()
                );
        }

        public static void Encode(NewCommonDefenseTreatyPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.sender);
            writer.Write(obj.receiver);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer senderPlayer = context.PlayerSender!;
                IPlayer receiverPlayer = server.GetPlayer(receiver)!;
                receiverPlayer.SendPacket(new NewCommonDefenseTreatyPacket(sender, receiver));
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                bool accepted = client.MsgBoxYesNo($"{sender} wants to create a common defense treaty with you");
                ConfirmCommonDefenseTreatyPacket packet = new ConfirmCommonDefenseTreatyPacket(sender, receiver, accepted);
                client.SendPacket(packet);
            }
        }
    }
}
