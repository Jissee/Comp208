using EoE.Network;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client
{
    internal class RemotePlayer : IPlayer
    {
        private Client client;
        public RemotePlayer(string playerName, Client client)
        {
            PlayerName = playerName;
            this.client = client;
        }

        public Socket Connection => throw new NotSupportedException("You can only send packet to RemotePlayer");

        public string PlayerName { get; set; }

        public bool IsConnected => true;

        public void SendPacket<T>(T packet) where T : IPacket<T>
        {
            client.Handler.SendPacket(packet, client.Socket, this);
        }
    }
}
