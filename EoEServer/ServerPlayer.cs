using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayer : IPlayer
    {
        private Socket connection;
        private Server server;
        private string name;
        public string PlayerName { 
            get 
            {
                return name;
            }
            set 
            { 
                if (name == null)
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Player name cannot be reset.");
                }
            } 
        }
        public bool IsAvailable => PlayerName != null;

        public bool IsConnected => !((connection.Poll(1000, SelectMode.SelectRead) && (connection.Available == 0)) || !connection.Connected);

        public ServerPlayer(Socket connection, Server server)
        {
            this.connection = connection;
            this.server = server;
        }

        public void Disconnect()
        {
            connection.Disconnect(true);
        }

        public int AvailableData()
        {
            return connection.Available;
        }

        public byte[] GetPacketBuf()
        {
            byte[] buf = new byte[connection.Available];
            int i = connection.Receive(buf);
            if(i <= 0)
            {
                throw new Exception("Packet not available");
            }
            return buf;
        }

        public void SendPacketRaw(byte[] data)
        {
            connection.Send(data);
        }
    }
}
