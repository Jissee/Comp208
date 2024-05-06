using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System.Net.Sockets;

namespace EoE
{
    public interface IPlayer : ITickable
    {
        Socket Connection { get; }
        IServer Server { get; }
        IServerGonveranceManager GonveranceManager { get; }
        bool IsLose { get; }
        bool IsBegin { get; }
        void GameLose();
        string PlayerName { get; set; }
        bool IsConnected { get; }
        void SendPacket<T>(T packet) where T : IPacket<T>;
        bool FinishedTick { get; set; }
        void BeginGame();
        void CloseSocket();
        void Disconnect();
    }
}
