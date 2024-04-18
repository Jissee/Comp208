using EoE.ClientInterface;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.TradeSystem;
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
        IClientTradeManager TradeManager { get; }
            List<string> OtherPlayer{ get;}
        public Dictionary<string, FieldListRecord> OtherPlayerFields { get; init; }
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
        IWindowManager WindowManager { get; }
        void SynchronizeOtherPlayersName(List<string> otherPlayers);
        void SynchronizePlayerName(string name);
        void SynchronizeOtherPlayerFieldLitst(string name, FieldListRecord record);
        void AddOthersChatMessage(string senderName, string message);
        void AddSelfChatMessage(string receiver, string message);
        IClientWarWidthList ClientWarWidthList { get; }
        IClientWarNameList ClientWarNameList { get; }

        IClientWarNameRelatedList ClientWarNameRelatedList { get; }
    }
}
