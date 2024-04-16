using EoE.ClientInterface;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Packets;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Entities
{
    public interface IClient : INetworkEntity
    {
        int TickCount { get; }
        string PlayerName { get; }
        IClientGonveranceManager GonveranceManager { get; }
        List<string> OtherPlayer{ get;}

        void SendPacket<T>(T packet) where T : IPacket<T>;
        void MsgBox(string msg);
        bool MsgBoxYesNo(string msg);
        IClientWarDeclarableList ClientWarDeclarableList { get; }
        IClientWarInformationList ClientWarInformationList { get; }
        IClientWarProtectorsList ClientWarProtectorsList { get; }
        IClientWarParticipatibleList ClientWarParticipatibleList { get; }
        IClientWarTargetList ClientWarTargetList { get; }
        IClientTreatyList ClientTreatyList { get; }
        void SynchronizeTickCount(int tickCount);
        void SynchronizePlayerName(List<string> otherPlayers);
        void SynchronizePlayerName(string name);
        IWindowManager WindowManager { get; }
    }
}
