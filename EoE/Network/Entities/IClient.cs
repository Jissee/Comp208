using EoE.ClientInterface;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Entities
{
    public interface IClient : INetworkEntity
    {
        string PlayerName { get; }
        public IClientGonveranceManager GonveranceManager { get; }
        List<string> OtherPlayer{ get;}

        void SendPacket<T>(T packet) where T : IPacket<T>;
        void MsgBox(string msg);
        void SynchronizePlayerName(string name, List<string> otherPlayers);
        void SynchronizePlayerName(string name);
        IWindowManager WindowManager { get; }
    }
}
