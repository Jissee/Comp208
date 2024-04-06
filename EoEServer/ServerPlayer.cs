using EoE.GovernanceSystem;
using EoE.Network;
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
        public Server Server { get; }
        private string name;
        public bool IsLose => GonveranceManager.IsLose;
        private Army army;
        public PlayerGonverance GonveranceManager { get; }
        public void AddArmy(Army army)
        {
            this.army = army;
        }
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
        public ServerPlayer(Socket connection, Server server)
        {
            this.Connection = connection;
            Server = server;
            GonveranceManager = new PlayerGonverance(server.Status);
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
        public void FillFrontier(int battle, int informative, int mechanism)
        {
            if (GonveranceManager.ResourceList.GetResourceCount(GameResourceType.BattleArmy) < battle)
            {
                // TODO:check
            }
            if (GonveranceManager.ResourceList.GetResourceCount(GameResourceType.InformativeArmy) < informative)
            {
                // TODO:
            }
            if (GonveranceManager.ResourceList.GetResourceCount(GameResourceType.MechanismArmy) < mechanism)
            {
                // TODO:
            }
        }

        public void GameLose()
        {
            //ToDo  Cleaning
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
