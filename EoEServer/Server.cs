using EoE.GovernanceSystem;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class Server : IServer, ITickable
    {
        public Socket ServerSocket { get; }

        private readonly IPEndPoint address;
        private bool isServerRunning;
        private bool isGameRunning;
        public ServerPacketHandler PacketHandler { get; }
        public GameStatus Status {get; private set;}
        public ServerPlayerList PlayerList { get; private set;}


        public Server(string ip, int port) 
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            PacketHandler = new ServerPacketHandler(this);
            PlayerList = new ServerPlayerList();
            isServerRunning = false;
            isGameRunning = false;
            

        }
        public void BeginGame()
        {
            Status = new GameStatus(500);
            isGameRunning = true;

            lock (PlayerList)
            {
                foreach (var player in PlayerList.Players)
                {
                    player.BeginGame();
                }
            }
        }

        public void Start()
        {
            lock(this)
            {
                ServerSocket.Bind(address);
                ServerSocket.Listen(6);
                isServerRunning = true;
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
                isServerRunning = false;
            }
        }

        public void ConnectionLoop()
        {
            while (isServerRunning)
            {   // Accept one connection
                Socket cl = ServerSocket.Accept();
                // Extract the IP adress and Port num of client
                EndPoint endp = cl.RemoteEndPoint;
                if(endp is IPEndPoint iPEndPoint)
                {
                    Console.WriteLine($"{iPEndPoint.Address.ToString()}:{iPEndPoint.Port} connecting.");
                }
                lock(PlayerList)
                {
                    PlayerList.PlayerLogin(new ServerPlayer(cl, this));
                }/*
                lock (Clients)
                {
                    Clients.Add(new ServerPlayer(cl, this));
                }*/
                
            }
        }
        public void DisconnectionLoop()
        {
            while (isServerRunning)
            {
                lock(PlayerList)
                {
                    PlayerList.HandlelayerDisconnection();
                }
                /*
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
                        }
                        
                    }
                }*/
            }
        }

        public void MessageLoop()
        {
            while (isServerRunning)
            {
                lock (PlayerList)
                {
                    PlayerList.HandlePlayerMessage(PacketHandler, this);
                }
            }
        }

        public void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>
        {
            lock (PlayerList)
            {
                PlayerList.Broadcast(packet, condition);
            }
        }

        public IPlayer? GetPlayer(string playerName)
        {
            return PlayerList.GetPlayer(playerName);
        }

        public void Tick()
        {
            Status.Tick();
            lock(PlayerList)
            {
                PlayerList.Tick();
            }
        }

        public void CheckPlayerTickStatus()
        {
            bool tickStatus;
            lock (PlayerList)
            {
                tickStatus = PlayerList.CheckPlayerTickStatus();
            }
            if (tickStatus)
            {
                Tick();
            }
        }

        public void InitPlayerName(IPlayer player, string name)
        {
            PlayerList.InitPlayerName((ServerPlayer)player, name);
        }
    }
}
