using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class Server : IServer, ITickable
    {
        public Socket ServerSocket { get; }

        public  List<IPlayer> Clients { get; }

        private readonly IPEndPoint address;
        private bool isRunning;
        public ServerPacketHandler PacketHandler { get; }
        public Server(string ip, int port) 
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            Clients = new List<IPlayer>();
            PacketHandler = new ServerPacketHandler(this);
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
            {   // Accept one connection
                Socket cl = ServerSocket.Accept();
                // Extract the IP adress and Port num of client
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
                        ServerPlayer c = (ServerPlayer)Clients[i];
                        bool b = c.IsConnected;
                        if (!b)
                        {
                            Console.WriteLine($"{c.PlayerName} logged out.");
                            Clients.Remove(c);
                            Broadcast(new RemotePlayerSyncPacket(c.PlayerName, false), (player) => true);
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
                        if (player.Connection.Available > 0)
                        {
                            byte[] lengthBuf = new byte[8];

                            player.Connection.Receive(lengthBuf);
                            MemoryStream msLen = new MemoryStream(lengthBuf);
                            BinaryReader br = new BinaryReader(msLen);
                            long length = br.ReadInt64();

                            byte[] buf = new byte[length];
                            int i = player.Connection.Receive(buf);
                            //Console.WriteLine(i);
                            PacketContext context = new PacketContext(NetworkDirection.Client2Server, player, this);
                            PacketHandler.ReceivePacket(buf, context);
                        }
                    }
                }
            }
        }

        public void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>
        {
            foreach (ServerPlayer player in Clients)
            {
                if (condition(player))
                {
                    player.SendPacket(packet);
                }
            }
        }

        public IPlayer? GetPlayer(string playerName)
        {
            foreach(ServerPlayer player in Clients)
            {
                if(player.PlayerName == playerName)
                {
                    return player;
                }
            }
            return null;
        }

        public void Tick()
        {
            foreach (ServerPlayer player in Clients)
            {
                player.Tick();
            }

        }

        public void CheckPlayerTickStatus()
        {
            bool allFinished = true;
            foreach (var item in Clients)
            {
                if (!item.FinishedTick)
                {
                    allFinished = false;
                }
            }

            if (allFinished)
            {
                this.Tick();
            }
        }
    }
}
