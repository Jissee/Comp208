using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Chat
{
    public class ChatPacket : IPacket<ChatPacket>
    {
        private string chatMessage;
        private string targetPlayer;
        public ChatPacket(string chatMessage, string targetPlayer)
        {
            this.chatMessage = chatMessage;
            this.targetPlayer = targetPlayer;
        }

        public static ChatPacket Decode(BinaryReader reader)
        {
            return new ChatPacket(reader.ReadString(), reader.ReadString());
        }

        public static void Encode(ChatPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.chatMessage);
            writer.Write(obj.targetPlayer);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Server2Client)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer target = server.GetPlayer(targetPlayer)!;
                target.SendPacket(new ChatPacket(chatMessage, context.PlayerSender!.PlayerName));
            }
            else
            {
                //todo: show messages
            }
        }
    }
}
