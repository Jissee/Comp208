﻿using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket;
using EoE.Server.Treaty;
using EoE.Server.WarSystem;
using EoE.TradeSystem;
using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayerList : IServerPlayerList, ITickable
    {
        public ITreatyManager TreatyManager { get; }
        public IWarManager WarManager { get; }
        public List<IPlayer> Players { get; }
        public IServerTradeManager TradeManager { get; }
        private IPlayer? host;
        private IServer server;
        private int playerCount = 1;
        public ServerPlayerList(IServer server) 
        { 
            TreatyManager = new TreatyManager(this);
            Players = new List<IPlayer>();
            WarManager = new WarManager(server);
            this.server = server;
        }

        public void SetPlayerCount(int playerCount)
        {
            this.playerCount = playerCount;
        }
        public void PlayerLogin(IPlayer player)
        {
            if (Players.Count <= playerCount)
            {
                if (host == null)
                {
                    host = player;
                    player.SendPacket(new RoomOwnerPacket(true));
                }
                Players.Add(player);
            }
            else
            {
                player.SendPacket(new ServerMessagePacket("Server full, please wait for the end of the existing match or for the host to increase the number of players"));
            }

        }

        public void PlayerLogout(IPlayer player)
        {
            Console.WriteLine($"{player.PlayerName} logged out.");
            Players.Remove(player);
            if(Players.Count == 0)
            {
                server.Stop();
                server.Restart();
            }
            else if (player == host)
            {
                host = Players[0];
                host.SendPacket(new RoomOwnerPacket(true));
            }
        }
        public void HandlePlayerDisconnection()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                IPlayer c = Players[i];
                bool b = c.IsConnected;
                if (!b)
                {
                    PlayerLogout(c);
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
                    packetHandler.ReceivePacket(buf, context);
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
            playerRef.PlayerName = name;
            Console.WriteLine($"{name} logged in");
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
            var gotProtectors = TreatyManager.PlayerRelation.GetProtectorsRecursively((ServerPlayer)target);
            protectors.AddRange(gotProtectors);
            return protectors;
        }

        public void Kickplayer(IPlayer player)
        {
            TradeManager.ClearAll(player);
            player.GonveranceManager.ClearAll();
            //TODo
            PlayerLogout(player);
        }
    }
}
