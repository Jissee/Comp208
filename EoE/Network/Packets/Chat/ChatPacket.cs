using EoE.GovernanceSystem.ClientInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EoE.Network.Packets.Chat
{
    public class ChatPacket : IPacket<ChatPacket>
    {
        private string chatMessage;
        private string receiverName;
        private string senderName;
        public ChatPacket(string chatMessage, string targetPlayer,string senderName)
        {
            this.chatMessage = chatMessage;
            this.receiverName = targetPlayer;
            this.senderName = senderName;
        }

        public static ChatPacket Decode(BinaryReader reader)
        {
            return new ChatPacket(reader.ReadString(), reader.ReadString(), reader.ReadString());
        }

        public static void Encode(ChatPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.chatMessage);
            writer.Write(obj.receiverName);
            writer.Write(obj.senderName);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer target = server.GetPlayer(receiverName)!;
                    target.SendPacket(new ChatPacket(chatMessage, context.PlayerSender!.PlayerName,context.PlayerSender.PlayerName));
                }
            }
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.AddOthersChatMessage(senderName, chatMessage);
                }
            }
        }
    }
}
