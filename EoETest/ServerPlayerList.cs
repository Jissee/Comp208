using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket;
using EoE.Server.TradeSystem;
using EoE.Server.Treaty;
using EoE.Server.WarSystem;
using EoE.TradeSystem;
using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayerList : IServerPlayerList, ITickable
    { 
        public List<IPlayer> Players { get; }
        public ITreatyManager TreatyManager { get; }
        public IWarManager WarManager { get; }
        public IServerTradeManager TradeManager { get; }
        public IPlayer? Host { get; private set; }
        private IServer server;
        public int PlayerCount { get; private set; } = 1;
        public bool AllBegins 
        { 
            get
            {
                bool begin = true;
                foreach(var player in Players)
                {
                    begin &= player.IsBegin;
                }
                return begin && Players.Count == PlayerCount;
            } 
        }


        public ServerPlayerList(IServer server) 
        {
            Players = new List<IPlayer>();
            TreatyManager = new TreatyManager(this);
            WarManager = new WarManager(server);
            TradeManager = new ServerTradeManager(server);
            this.server = server;
        }
        public void GameBegin()
        {
            TreatyManager.BuildPlayerProtected();
            foreach (IPlayer player1 in Players)
            {
                Dictionary<IPlayer, WarTarget> temp = new Dictionary<IPlayer, WarTarget>();
                WarManager.WarTargets.Add(player1, temp);
                foreach (IPlayer player2 in Players)
                {
                    WarManager.WarTargets[player1].Add(player2, new WarTarget());
                }
            }
        }
        public void SetPlayerCount(int playerCount)
        {
            this.PlayerCount = playerCount;
        }
        public void PlayerLogin(IPlayer player)
        {
            if (Players.Count < PlayerCount)
            {
                Players.Add(player);
            }
            else
            {
                player.SendPacket(new ServerMessagePacket(ServerMessagePacket.SERVER_FULL));
                player.Disconnect();
            }
        }

        
        public void PlayerLogout(IPlayer player)
        {
            IServer.Log("Connection", $"{player.PlayerName} logged out.");
            Players.Remove(player);
            if(Players.Count == 0)
            {
                server.Stop();
                //server.Restart();

            }
            else if (player == Host)
            {
                Host = Players[0];
            }
            player.CloseSocket();
            var nameEnum = from thisPlayer in Players
                           select thisPlayer.PlayerName;
            List<string> list = [.. nameEnum];
            server.Boardcast(new PlayerListPacket(list, list.Count), player => true);
        }
        public void HandlePlayerDisconnection()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                IPlayer c = Players[i];
                bool b = c.IsConnected;
                if (!b)
                {
                    c.GameLose();
                }
            }
        }
        public void HandlePlayerMessage(PacketHandler packetHandler, IServer server)
        {
            foreach (IPlayer player in Players)
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
                    PacketContext context = new PacketContext(NetworkDirection.Client2Server, player, server);
                    string fromName = player.PlayerName;
                    if(fromName == null)
                    {
                        EndPoint? endpoint = player.Connection.RemoteEndPoint;
                        if(endpoint != null)
                        {
                            fromName = endpoint.ToString();
                        }
                        else
                        {
                            fromName = "null";
                        }
                    }
                    packetHandler.ReceivePacket(buf, context, fromName);
                }
            }
        }

        public void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>
        {
            foreach (IPlayer player in Players)
            {
                if (condition(player))
                {
                    player.SendPacket(packet);
                }
            }
        }

        public IPlayer? GetPlayer(string name)
        {
            foreach (var player in Players)
            {
                if(player.PlayerName == name) return player;
            }
            return null;
        }
        public void InitPlayerName(IPlayer playerRef, string name)
        {
            
            if (Host == null)
            {
                playerRef.PlayerName = name;
                Host = playerRef;
                playerRef.SendPacket(new EnterRoomPacket(true));
            }
            else
            {
                bool nameCheck = true;
                while (CheckName(name) == false)
                {
                    name += "(1)";
                    nameCheck = false;
                }
                playerRef.PlayerName = name;
                if (nameCheck == false)
                {
                    playerRef.SendPacket(new PlayerLoginPacket(name));
                    playerRef.SendPacket(new ServerMessagePacket("Due to name conflict, your player name has been reset to " + name));
                }
                
                playerRef.SendPacket(new EnterRoomPacket(false));
            }
            IServer.Log("Connection", $"{name} logged in");
            server.Boardcast(new GameSettingPacket(new GameSettingRecord(server.PlayerList.PlayerCount, server.Status.TotalTick)),playerRef=>true);
            var nameEnum = from player in Players
                           select player.PlayerName;
            List<string> list = [.. nameEnum];
            server.Boardcast(new PlayerListPacket(list,list.Count),player=>true);
        }

        private bool CheckName(string name)
        {
            foreach (IPlayer player in Players)
            {
                if (player.PlayerName == name)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckPlayerTickStatus()
        {
            bool allFinished = true;
            foreach (var item in Players)
            {
                if (!item.FinishedTick)
                {
                    allFinished = false;
                }
            }
            return allFinished;
        }
        

        public void Tick()
        {
            foreach (IPlayer player in Players)
            {
                player.Tick();
            }
            TreatyManager.Tick();
            WarManager.Tick();
        }

        public List<IPlayer> GetProtectorsRecursively(IPlayer target)
        {
            List<IPlayer> protectors = new List<IPlayer>();
            var gotProtectors = TreatyManager.PlayerRelation.GetProtectorsRecursively(target);
            protectors.AddRange(gotProtectors);
            return protectors;
        }


    }
}
