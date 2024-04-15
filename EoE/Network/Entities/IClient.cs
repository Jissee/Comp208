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
    }
}
