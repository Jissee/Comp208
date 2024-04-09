using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayerList : ITickable
    {
        public List<ServerPlayer> Players { get; }

        public ServerPlayerList() 
        { 
            Players = new List<ServerPlayer>();
        }

        public void PlayerLogin(ServerPlayer player)
        {
            Players.Add(player);
        }

        public void PlayerLogout(ServerPlayer player)
        {
            Console.WriteLine($"{player.PlayerName} logged out.");
            Players.Remove(player);
        }
        public void HandlelayerDisconnection()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                ServerPlayer c = Players[i];
                bool b = c.IsConnected;
                if (!b)
                {
                    PlayerLogout(c);
                }
            }
        }
        public void HandlePlayerMessage(PacketHandler packetHandler, Server server)
        {
            foreach (ServerPlayer player in Players)
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
            foreach (ServerPlayer player in Players)
            {
                if (condition(player))
                {
                    player.SendPacket(packet);
                }
            }
        }

        public ServerPlayer? GetPlayer(string name)
        {
            foreach (var player in Players)
            {
                if(player.PlayerName == name) return player;
            }
            return null;
        }
        public void InitPlayerName(ServerPlayer playerRef, string name)
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
            foreach (ServerPlayer player in Players)
            {
                player.Tick();
            }
        }
    }
}
