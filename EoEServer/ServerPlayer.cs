using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Server.GovernanceSystem;
using EoE.Server.WarSystem;
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
        public IServer Server { get; }
        private string name;
        public bool IsLose => GonveranceManager.IsLose;

        public IServerGonveranceManager GonveranceManager { get; private set; }

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
        public ServerPlayer(Socket connection, IServer server)
        {
            this.Connection = connection;
            Server = server;
            
        }
        public void BeginGame()
        {
            GonveranceManager = new ServerPlayerGonverance(Server.Status,100,this,Server);
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

        public void GameLose()
        {
            Server.PlayerList.WarManager.PlayerLose(this);
            // todo: treatymanage.playerlose
            // todo: trademenager.playerlose
        }


        public void Tick()
        {
            GonveranceManager.Tick();
            if (IsLose)
            {
                GameLose();
            }
        }
    }
}
