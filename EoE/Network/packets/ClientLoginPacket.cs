using EoE.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Network
{
    public class ClientLoginPacket : IPacket<ClientLoginPacket>
    {
        private string playerName;
        public ClientLoginPacket(string playerName)
        {
            this.playerName = playerName;
        }
        public static ClientLoginPacket Decode(BinaryReader reader)
        {
            return new ClientLoginPacket(reader.ReadString());
        }

        public static void Encode(ClientLoginPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.playerName);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if(ne is IServer server)
                {
                    foreach(IPlayer player in server.Clients)
                    {
                        if(player == context.PlayerSender)
                        {
                            player.PlayerName = playerName;
                            Console.WriteLine($"{playerName} logged in");
                            server.SendPacket(new ClientLoginPacket(playerName), player);
                            server.Broadcast(new ClientLoginPacket(playerName), (player1) => player1 != player);
                        }
                    }
                }
            }
            else
            {
                IClient ne = (IClient)context.Receiver;
                ne.MsgBox($"{playerName}, has logged in.");
            }
        }
    }
}
