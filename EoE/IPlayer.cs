using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE
{
    public interface IPlayer: ITickable
    {
        Socket Connection { get; }
        IServer Server { get; }
        IServerGonveranceManager GonveranceManager { get; }
        bool IsLose { get; }
        void GameLose();
        string PlayerName { get; set; }
        bool IsConnected { get; }
        void SendPacket<T>(T packet) where T : IPacket<T>;
        bool FinishedTick { get; set; }
        void BeginGame();
    }
}
