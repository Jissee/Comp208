using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class Server : IServer
    {
        public Socket ServerSocket { get; }

        public List<IPlayer> Clients { get; }

        private IPEndPoint address;
        //private List<Socket> pendingClients;
        private bool isRunning;
        private ServerPacketHandler handler;
        public Server(string ip, int port) 
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            Clients = new List<IPlayer>();
            handler = new ServerPacketHandler(this);
        }

        public void Start()
        {
            lock(this)
            {
                ServerSocket.Bind(address);
                ServerSocket.Listen(6);
                isRunning = true;
                Task.Run(ConnectionLoop);
                Task.Run(DisconnectionLoop);
                Task.Run(MessageLoop);
                Console.WriteLine("Server started.");
            }
        }
        public void Stop()
        {
            lock(this)
            {
                ServerSocket?.Close();
                isRunning = false;
            }
        }

        public void ConnectionLoop()
        {
            while (isRunning)
            {
                Socket cl = ServerSocket.Accept();
                EndPoint endp = cl.RemoteEndPoint;
                if(endp is IPEndPoint iPEndPoint)
                {
                    Console.WriteLine($"{iPEndPoint.Address.ToString()}:{iPEndPoint.Port} connecting.");
                }
                lock (Clients)
                {
                    Clients.Add(new ServerPlayer(cl, this));
                }
                
            }
        }
        public void DisconnectionLoop()
        {
            while (isRunning)
            {
                lock (Clients)
                {
                    for(int i = 0; i < Clients.Count; i++)
                    {
                        ServerPlayer player = (ServerPlayer)Clients[i];
                        bool b = player.IsConnected;
                        if (!b)
                        {
                            Console.WriteLine(player + $" disconnected, name: {player.PlayerName}");
                            Clients.Remove(player);
                        }
                        
                    }
                }
            }
        }

        public void MessageLoop()
        {
            while (isRunning)
            {
                lock (Clients)
                {
                    foreach (ServerPlayer player in Clients)
                    {
                        int available = player.AvailableData();
                        if (available > 0)
                        {
                            byte[] buf = player.GetPacketBuf();
                            //Console.WriteLine(i);
                            PacketContext context = new PacketContext(NetworkDirection.Client2Server, player, this);
                            handler.ReceivePacket(buf, context);
                        }
                    }
                }
            }
        }

        public void SendPacket<T>(T packet, string playerName) where T : IPacket<T>
        {
            lock (Clients)
            {
                foreach(ServerPlayer player in Clients)
                {
                    if(player.PlayerName == playerName && player.IsConnected)
                    {
                        byte[] data = handler.PreparePacket(packet);
                        player.SendPacketRaw(data);
                    }
                }
            }

        }
        public void SendPacket<T>(T packet, IPlayer player) where T : IPacket<T>
        {
            SendPacket(packet, player.PlayerName);
        }

        public void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>
        {
            lock (Clients)
            {
                foreach (ServerPlayer player in Clients)
                {
                    if (condition(player))
                    {
                        SendPacket<T>(packet, player);
                    }
                }
            }
        }
    }
}
