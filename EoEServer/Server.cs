using EoE.Events;
using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
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
        private bool needRestart;
        public PacketHandler PacketHandler { get; }
        public EventList EventList { get; }
        public GameStatus Status {get; private set;}
        public IServerPlayerList PlayerList { get; private set;}

        public Server(string ip, int port) 
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            PacketHandler = new ServerPacketHandler(this);
            EventList = new EventList();
            PlayerList = new ServerPlayerList(this);
            isServerRunning = false;
            isGameRunning = false;
            needRestart = false;
            Status = new GameStatus(500, 100);
        }
        public void BeginGame()
        {
            isGameRunning = true;
            PrepareResourceBonusEvents();
            for (int i = 0; i < Math.Max(1,Status.TotalTick); i++)
            {
                foreach (IPlayer player in PlayerList.Players)
                {
                    Random random = new Random();
                    int index = random.Next(1, 6);
                    PreparePlayerRandomEvents(player, index);
                }
            }
            PrepareGlobalBonusEvents();

            foreach (IPlayer player in PlayerList.Players)
            {
                player.SendPacket(new EnterGamePacket());
                player.SendPacket(new ResourceUpdatePacket(new ResourceListRecord(player.GonveranceManager.ResourceList)));
                player.SendPacket(new FieldUpdatePacket(new FieldListRecord(player.GonveranceManager.FieldList)));
                player.SendPacket(new PopulationUpdatePacket(player.GonveranceManager.PopManager.GetPopulationRecord()));
            }
        }
        private void PrepareGlobalBonusEvents()
        {
            Event.Builder builder1 = new Event.Builder();
            builder1.ForServer(this)
                .IfServer(server => true)
                .IfPlayer(thePlayer => true)
                .HappenIn((int)(Status.TotalTick * 0.25f))
                .LastFor(1)
                .Do
                (
                   (server, player) =>
                   {
                       player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("", 2.5);
                       player.SendPacket(new ServerMessagePacket("Due to a new technological breakthrough, " +
                           "the productivity of all country's four primary resources has been increased."));
                   }
                 );
            EventList.AddEvent(builder1.Build());

            Event.Builder builder2 = new Event.Builder();
            builder2.ForServer(this)
                .IfServer(server => true)
                .IfPlayer(thePlayer => true)
                .HappenIn((int)(Status.TotalTick * 0.25f))
                .LastFor(1)
                .Do
                (
                   (server, player) =>
                   {
                       player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("", -2.5);
                       player.SendPacket(new ServerMessagePacket("Due to the pandemic, " +
                           "the productivity of all country's four primary resources has been decreased."));
                   }
                 );
            EventList.AddEvent(builder2.Build());
        }
        private void PrepareResourceBonusEvents()
        {
            
            Event.Builder builder1 = new Event.Builder();
            builder1.ForServer(this)
                .IfServer(server => true)
                .IfPlayer(player => true)
                .HappenIn((int)(Status.TotalTick * 0.1f))
                .LastFor(1)
                .Do
                (
                   (server, _) =>
                   {
                       Random random = new Random();
                       foreach (IPlayer player in server.PlayerList.Players)
                       {
                           int index = random.Next(0, 5);
                           switch (index)
                           {
                               case 0:
                                   player.GonveranceManager.PlayerStatus.CountrySiliconModifier.AddValue("",15);
                                   player.SendPacket(new ServerMessagePacket("You gain a Silicon generation bonus"));
                                   break;
                               case 1:
                                   player.GonveranceManager.PlayerStatus.CountryCopperModifier.AddValue("", 15);
                                   player.SendPacket(new ServerMessagePacket("You gain a Copper generation bonus"));
                                   break;
                               case 2:
                                   player.GonveranceManager.PlayerStatus.CountryIronModifier.AddValue("", 15);
                                   player.SendPacket(new ServerMessagePacket("You gain a Iron generation bonus"));
                                   break;
                               case 3:
                                   player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("", 15);
                                   player.SendPacket(new ServerMessagePacket("You gain a luminum generation bonus"));
                                   break;
                               case 4:
                                   player.GonveranceManager.PlayerStatus.CountryElectronicModifier.AddValue("", 7.5);
                                   player.SendPacket(new ServerMessagePacket("You gain a Electronic generation bonus"));
                                   break;
                               case 5:
                                   player.GonveranceManager.PlayerStatus.CountryIndustryModifier.AddValue("", 7.5);
                                   player.SendPacket(new ServerMessagePacket("You gain a Industry generation bonus"));
                                   break;
                           }
                       }
                   }
                );
            EventList.AddEvent(builder1.Build());


        }

        private void PreparePlayerRandomEvents(IPlayer player, int eventNumber)
        {
            
            switch (eventNumber)
            {
                case 1:
                    Event.Builder builder1 = new Event.Builder();
                    builder1.ForPlayer(player)
                        .IfServer(server => true)
                        .IfPlayer(thePlayer => true)
                        .HappenIn((int)(Status.TotalTick * 0.25f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("", 2.5);
                               player.SendPacket(new ServerMessagePacket("Under your diligent and dedicated leadership, " +
                                   "the productivity of your country's four primary resources has been increased."));
                           }
                         );
                    EventList.AddEvent(builder1.Build());
                    break;
                case 2:
                    Event.Builder builder2 = new Event.Builder();
                    builder2.ForPlayer(player)
                        .IfServer(server => true)
                        .IfPlayer(thePlayer => true)
                        .HappenIn((int)(Status.TotalTick * 0.25f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("", -2.5);
                               player.SendPacket(new ServerMessagePacket("Under your diligent and dedicated leadership, " +
                                   "the productivity of your country's four primary resources has been reduced."));
                           }
                         );
                    EventList.AddEvent(builder2.Build());
                    break;
                case 3:
                    Event.Builder builder3 = new Event.Builder();
                    builder3.ForPlayer(player)
                        .IfServer(server => true)
                        .IfPlayer(thePlayer => true)
                        .MeanTimeToHappen((int)(Status.TotalTick * 0.3f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountrySecondaryModifier.AddValue("", 1.0);
                               player.SendPacket(new ServerMessagePacket("Under your diligent and dedicated leadership, " +
                                   "the productivity of your country's two secondary resources has been reduced."));
                           }
                         );
                    EventList.AddEvent(builder3.Build());
                    break;
                case 4:
                    Event.Builder builder4 = new Event.Builder();
                    builder4.ForPlayer(player)
                        .IfServer(server => true)
                        .IfPlayer(thePlayer => true)
                        .HappenIn((int)(Status.TotalTick * 0.25f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountryCopperModifier.AddValue("", -3);
                               player.SendPacket(new ServerMessagePacket("Due to the onslaught of severe weather conditions, " +
                                   "the productivity of Copper has been reduced."));
                           }
                         );
                    EventList.AddEvent(builder4.Build());
                    break;
                case 5:
                    Event.Builder builder5 = new Event.Builder();
                    builder5.ForPlayer(player)
                        .IfServer(server => true)
                        .IfPlayer(thePlayer => true)
                        .HappenIn((int)(Status.TotalTick * 0.25f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("", 3);
                               player.SendPacket(new ServerMessagePacket("Due to a new technological breakthrough, " +
                                   "the productivity of Aluminum has been increased."));
                           }
                         );
                    EventList.AddEvent(builder5.Build());
                    break;
                case 6:
                    Event.Builder builder6 = new Event.Builder();
                    builder6.ForPlayer(player)
                       .IfServer(server => true)
                       .IfPlayer(thePlayer => true)
                       .HappenIn((int)(Status.TotalTick * 0.25f))
                       .LastFor(1)
                       .Do
                       (
                          (server, player) =>
                          {
                              player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("", -3);
                              player.SendPacket(new ServerMessagePacket("Due to the onslaught of severe weather conditions, " +
                                  "the productivity of Aluminum has been reduced.."));
                          }
                        );
                    EventList.AddEvent(builder6.Build());
                    break;
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
                IServer.Log("Server", "Server started.");
            }
        }
        public void Stop()
        {
            lock(ServerSocket)
            {
                ServerSocket?.Close();
                isServerRunning = false;
            }
        }

        public void ConnectionLoop()
        {
            while (isServerRunning)
            {   // Accept one connection
                Socket cl ;
                lock (ServerSocket)
                {
                    if (needRestart)
                    {
                        break;
                    }
                    cl = ServerSocket.Accept();
                }
                // Extract the IP adress and Port num of client
                EndPoint endp = cl.RemoteEndPoint;

                IServer.Log("Connection", $"{endp} connecting.");

                lock(PlayerList)
                {
                    PlayerList.PlayerLogin(new ServerPlayer(cl, this));
                }
                
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

        public void Boardcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>
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

        public bool IsNeedRestart()
        {
            return needRestart;
        }
        public void Restart()
        {
            needRestart = true;
        }

        public void SetGame(int playerCount, int totalTick)
        {
            PlayerList.SetPlayerCount(playerCount);
            Status.TotalTick = totalTick;
        }
    }
}
