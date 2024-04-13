using EoE.GovernanceSystem.ServerInterface;
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

        IServerGonveranceManager GonveranceManager { get; }
        string PlayerName { get; set; }
        bool IsConnected { get; }
        void SendPacket<T>(T packet) where T : IPacket<T>;
        void FillFrontier(int battle, int informative, int mechanism); 
        bool FinishedTick { get; set; }
        void BeginGame();
    }
}
