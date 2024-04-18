using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Server.GovernanceSystem;
using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayer : IPlayer, ITickable
    {
        public Socket Connection { get; }
        public IServer Server { get; }
        private string? name;
        public bool IsLose => GonveranceManager.IsLose;
        public bool IsBegin { get; private set; } = false;

        public IServerGonveranceManager GonveranceManager { get; private set; }

        public string? PlayerName { 
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
            GonveranceManager = new ServerPlayerGonverance(Server.Status, 10000, this);

        }
        public void BeginGame()
        {
            IsBegin = true;
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
                    handler.SendPacket(packet, Connection, PlayerName);
                }
            }

        }

        public void GameLose()
        {
            if (Server.isGameRunning)
            {
                Server.Boardcast(new ServerMessagePacket($"{this.PlayerName} lost the game."), player => true);
            }
            else
            {
                Server.Boardcast(new ServerMessagePacket($"{this.PlayerName} leave the game."), player => true);
            }
             
            GonveranceManager.ClearAll();
            Server.PlayerList.WarManager.PlayerLose(this);
            Server.PlayerList.TradeManager.ClearAll(this);
            Server.Boardcast(new OtherPlayerFieldUpdate(new FieldListRecord(GonveranceManager.FieldList), this.PlayerName), thisPlayer => thisPlayer.PlayerName != this.PlayerName);
            //TodO TreatyManager losse
            Server.PlayerList.PlayerLogout(this);
        }


        public void Tick()
        {
            FinishedTick = false;
            GonveranceManager.Tick();
            if (IsLose)
            {
                GameLose();
            }
        }

        public void CloseSocket()
        {
            Connection.Shutdown(SocketShutdown.Both);
            Connection.Close();
        }
        public void Disconnect()
        {
            Server.PlayerList.PlayerLogout(this);
        }
    }
}
