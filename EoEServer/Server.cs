using EoE.Governance;
using EoE.Governance.Interface;
using EoE.Governance.ServerInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.TradePacket;
using EoE.Server.Events;
using EoE.Server.Network;
using EoE.Server.War;
using System.Net;
using System.Net.Sockets;

namespace EoE.Server
{
    public class Server : IServer, ITickable
    {
        public Socket ServerSocket { get; }

        private readonly IPEndPoint address;
        private bool isServerRunning;
        public bool IsGameRunning { get; private set; }
        private bool needRestart;
        public PacketHandler PacketHandler { get; }
        public EventList EventList { get; }
        public GameStatus Status { get; private set; }
        public IServerPlayerList PlayerList { get; private set; }

        public Server(string ip, int port)
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            PacketHandler = new ServerPacketHandler(this);
            EventList = new EventList();
            PlayerList = new ServerPlayerList(this);
            isServerRunning = false;
            IsGameRunning = false;
            needRestart = false;
            Status = new GameStatus(500, 100);
        }
        public void BeginGame()
        {
            IsGameRunning = true;
            PrepareResourceBonusEvents();
            Random random = new Random();
            foreach (IPlayer player in PlayerList.Players)
            {
                List<int> numberToSelected = new List<int>() { 1, 2, 3, 4, 5, 6 };
                for (int i = 0; i < Math.Max(1, Status.TotalTick * 0.05); i++)
                {
                    int index = random.Next(1, 6);
                    if (numberToSelected.Contains(index))
                    {
                        numberToSelected.Remove(index);
                        PreparePlayerRandomEvents(player, index);
                    }
                }
            }
            PrepareGlobalBonusEvents();

            foreach (IPlayer player in PlayerList.Players)
            {
                player.SendPacket(new EnterGamePacket());
                player.SendPacket(new ResourceUpdatePacket(player.GonveranceManager.ResourceList.GetResourceListRecord()));
                player.SendPacket(new FieldUpdatePacket(player.GonveranceManager.FieldList.GetFieldListRecord()));
                player.SendPacket(new PopulationUpdatePacket(player.GonveranceManager.PopManager.GetPopulationRecord()));
                Boardcast(new OtherPlayerFieldUpdate(player.GonveranceManager.FieldList.GetFieldListRecord(), player.PlayerName), thisPlayer => thisPlayer != player);
            }
            PlayerList.GameBegin();
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
                       server.Status.GlobalAluminumModifier.AddValue("Primary Breakthrough", 2.5);
                       server.Boardcast(new ServerMessagePacket("Due to a new technological breakthrough, " +
                           "the productivity of all country's four primary resources has been increased."), player => true);
                   }
                 );
            EventList.AddEvent(builder1.Build());

            Event.Builder builder2 = new Event.Builder();
            builder2.ForServer(this)
                .IfServer(server => true)
                .IfPlayer(thePlayer => true)
                .HappenIn((int)(Status.TotalTick * 0.75f))
                .LastFor(1)
                .Do
                (
                   (server, player) =>
                   {
                       server.Status.GlobalAluminumModifier.AddValue("Pandamic", -2.5);
                       server.Boardcast(new ServerMessagePacket("Due to the pandemic, " +
                           "the productivity of all country's four primary resources has been decreased."), player => true);
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
                                   player.GonveranceManager.PlayerStatus.CountrySiliconModifier.AddValue("Silicon Bonus", 8);
                                   player.SendPacket(new ServerMessagePacket("You gain a Silicon generation bonus"));
                                   break;
                               case 1:
                                   player.GonveranceManager.PlayerStatus.CountryCopperModifier.AddValue("Copper Bonus", 8);
                                   player.SendPacket(new ServerMessagePacket("You gain a Copper generation bonus"));
                                   break;
                               case 2:
                                   player.GonveranceManager.PlayerStatus.CountryIronModifier.AddValue("Iron Bonus", 8);
                                   player.SendPacket(new ServerMessagePacket("You gain a Iron generation bonus"));
                                   break;
                               case 3:
                                   player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("Aluminum Bonus", 8);
                                   player.SendPacket(new ServerMessagePacket("You gain a aluminum generation bonus"));
                                   break;
                               case 4:
                                   player.GonveranceManager.PlayerStatus.CountryElectronicModifier.AddValue("Electronic Bonus", 4);
                                   player.SendPacket(new ServerMessagePacket("You gain a Electronic generation bonus"));
                                   break;
                               case 5:
                                   player.GonveranceManager.PlayerStatus.CountryIndustralModifier.AddValue("Industrial Bonus", 4);
                                   player.SendPacket(new ServerMessagePacket("You gain a Industrial generation bonus"));
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
                               player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("Primary Increase", 2.5);
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
                               player.GonveranceManager.PlayerStatus.CountryPrimaryModifier.AddValue("Primary Reduce", -2.5);
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
                        .HappenIn((int)(Status.TotalTick * 0.3f))
                        .LastFor(1)
                        .Do
                        (
                           (server, player) =>
                           {
                               player.GonveranceManager.PlayerStatus.CountrySecondaryModifier.AddValue("Secondary Reduce", 1.0);
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
                               player.GonveranceManager.PlayerStatus.CountryCopperModifier.AddValue("Bad Copper Weather", -3);
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
                               player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("Aluminium Breakthrough", 3);
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
                              player.GonveranceManager.PlayerStatus.CountryAluminumModifier.AddValue("Bad Aluminum Weather", -3);
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
            lock (this)
            {
                ServerSocket.Bind(address);
                ServerSocket.Listen(6);
                isServerRunning = true;
                Task.Run(ConnectionLoop);
                Task.Run(DisconnectionLoop);
                Task.Run(MessageLoop);
                IServer.Log("Server", $"server started on {address}");
            }
        }
        public void Stop()
        {
            try
            {
                ServerSocket?.Close();
                isServerRunning = false;
                needRestart = true;
            }
            catch (Exception ex)
            {
                IServer.Log("Server", "Cannot stop the server", ex);
            }

        }

        public void ConnectionLoop()
        {
            while (isServerRunning)
            {   // Accept one connection
                if (needRestart)
                {
                    break;
                }
                Socket cl;
                lock (ServerSocket)
                {
                    try
                    {
                        cl = ServerSocket.Accept();
                        EndPoint? endp = cl.RemoteEndPoint;

                        IServer.Log("Connection", $"{(endp == null ? "Unknown IP" : endp)} connecting.");

                        lock (PlayerList)
                        {
                            PlayerList.PlayerLogin(new ServerPlayer(cl, this));
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                // Extract the IP adress and Port num of client


            }
        }
        public void DisconnectionLoop()
        {
            while (isServerRunning)
            {
                lock (PlayerList)
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
            try
            {
                Status.Tick();
                if (Status.TickCount == Status.TotalTick)
                {
                    GameSummary();
                }
                lock (PlayerList)
                {
                    PlayerList.Tick();
                }
                EventList.Tick();
                Boardcast(new FinishTickPacket(true, Status.TickCount), player => true);
                foreach (IPlayer player in PlayerList.Players)
                {
                    Boardcast(new OtherPlayerFieldUpdate(player.GonveranceManager.FieldList.GetFieldListRecord(), player.PlayerName), thisPlayer => thisPlayer != player);
                }
                Boardcast(new OpenTransactionSynchronizePacket(PlayerList.TradeManager.OpenOrders.Count, PlayerList.TradeManager.OpenOrders), player => true);
            }
            catch (Exception ex)
            {
                IServer.Log("Tick Error", "Tick encountered an exception:", ex);
            }
            IServer.Log("Game", $"Tick {Status.TickCount} finished.");
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

        public void GameSummary()
        {
            // name, resource score, field score, pop score, army score, total score
            List<(string, int, int, int, int, int)> ranks = new List<(string, int, int, int, int, int)>();
            foreach (var player in PlayerList.Players)
            {
                IResourceList resourceList = player.GonveranceManager.ResourceList;
                int silicon = resourceList.GetResourceCount(GameResourceType.Silicon);
                int copper = resourceList.GetResourceCount(GameResourceType.Copper);
                int iron = resourceList.GetResourceCount(GameResourceType.Iron);
                int aluminum = resourceList.GetResourceCount(GameResourceType.Aluminum);
                int electronic = resourceList.GetResourceCount(GameResourceType.Electronic);
                int industrial = resourceList.GetResourceCount(GameResourceType.Industrial);

                int resourceScore =
                    silicon +
                    copper +
                    iron +
                    aluminum +
                    electronic * 2 +
                    industrial * 2;

                int battle = resourceList.GetResourceCount(GameResourceType.BattleArmy);
                int informative = resourceList.GetResourceCount(GameResourceType.InformativeArmy);
                int mechanism = resourceList.GetResourceCount(GameResourceType.MechanismArmy);

                int armyScore =
                    battle * new BattleArmyInfo().Worth +
                    informative * new InformativeArmyInfo().Worth +
                    mechanism * new MechanismArmyInfo().Worth;

                IFieldList fieldList = player.GonveranceManager.FieldList;
                int siliconf = fieldList.GetFieldCount(GameResourceType.Silicon);
                int copperf = fieldList.GetFieldCount(GameResourceType.Copper);
                int ironf = fieldList.GetFieldCount(GameResourceType.Iron);
                int aluminumf = fieldList.GetFieldCount(GameResourceType.Aluminum);
                int electronicf = fieldList.GetFieldCount(GameResourceType.Electronic);
                int industrialf = fieldList.GetFieldCount(GameResourceType.Industrial);

                int fieldScore =
                    siliconf +
                    copperf +
                    ironf +
                    aluminumf +
                    electronicf +
                    industrialf;



                IPopManager popManager = player.GonveranceManager.PopManager;
                int pop = popManager.TotalPopulation;
                int popScore = pop * 2;

                int totalScore = resourceScore + armyScore + fieldScore + popScore;

                ranks.Add((player.PlayerName, resourceScore, fieldScore, popScore, armyScore, totalScore));
            }

            ranks.Sort((tuple1, tuple2) => tuple2.Item6.CompareTo(tuple1.Item6));
            Boardcast(new GameSummaryPacket(ranks), player => true);
        }
    }
}
