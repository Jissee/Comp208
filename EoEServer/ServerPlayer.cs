using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayer : IPlayer, ITickable
    {
        public Socket Connection { get; }
        public Server Server { get; }
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
        public PlayerFieldList fieldList;
        public PlayerResourceList resourceList;
        public ServerPlayer(Socket connection, Server server)
        {
            this.Connection = connection;
            Server = server;
        }

        public bool IsConnected => !((Connection.Poll(1000, SelectMode.SelectRead) && (Connection.Available == 0)) || !Connection.Connected);

        public bool FinishedTick { get; set; }

        public void SendPacket<T>(T packet) where T : IPacket<T>
        {
            if (IsConnected)
            {
                PacketHandler handler = Server.PacketHandler;
                if (handler != null)
                {
                    handler.SendPacket(packet, Connection, null);
                }
            }

        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
