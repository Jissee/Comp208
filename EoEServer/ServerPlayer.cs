using EoE.GovernanceSystem.ServerInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket;
using EoE.Server.GovernanceSystem;
using System.Net.Sockets;

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

        public string? PlayerName
        {
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
            if (Server.IsGameRunning)
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
            Server.Boardcast(new OtherPlayerFieldUpdate(GonveranceManager.FieldList.GetFieldListRecord(), this.PlayerName), thisPlayer => thisPlayer.PlayerName != this.PlayerName);
            Server.PlayerList.TreatyManager.ClearAll(this);
            Server.PlayerList.PlayerLogout(this);
        }


        public void Tick()
        {
            FinishedTick = false;
            GonveranceManager.Tick();
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
