using EoE.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client
{
    internal class Client
    {
        public Socket Socket { get;}
        public int PlayerId { get;}
        private ClientPacketHandler packetHandler;
        public Client(int playerId) 
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            packetHandler = new ClientPacketHandler(this);
            PlayerId = playerId;
        }
        public void Connect(string host, int port)
        {
            Socket.Connect(host, port);
        }
        
    }
}
