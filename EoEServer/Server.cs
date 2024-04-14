using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Server.Events;
using EoE.Server.GovernanceSystem;
using EoE.Server.Network;
using EoE.Server.TradeSystem;
using EoE.TradeSystem;
using EoE.Treaty;
using EoE.WarSystem.Interface;
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
        public EventList EventList { get; }
        public GameStatus Status {get; private set;}
        public IServerPlayerList PlayerList { get; private set;}

        public IServerTradeManager TradeManager { get; private set; }



        public Server(string ip, int port) 
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            PacketHandler = new ServerPacketHandler(this);
            EventList = new EventList();
            PlayerList = new ServerPlayerList(this);
            isServerRunning = false;
            isGameRunning = false;
        }
        public void BeginGame()
        {
            TradeManager = new ServerTradeManager(this);
            Status = new GameStatus(500,100);
            isGameRunning = true;

            lock (PlayerList)
            {
                foreach (var player in PlayerList.Players)
                {
                    player.BeginGame();
                }
               // Event.Builder builder = new Event.Builder();
                //builder.ForServer(this).IfServer(server => true).IfPlayer(player => true);
               // EventList.AddEvent(builder.Build());
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
                    PlayerList.HandlePlayerDisconnection();
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
            bool tickStatus = false;
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

        public void SetGame(int playerCount, int totalTick)
        {
            PlayerList.SetPlayerCount(playerCount);
            Status.SetTotalTick (totalTick);
        }
    }
}
