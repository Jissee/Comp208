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
        IClientWarManager WarManager { get; }
        IClientTreatyList ClientTreatyList { get; }
        IWindowManager WindowManager { get; }
        List<string> OtherPlayer{ get;}
        Dictionary<string, FieldListRecord> OtherPlayerFields { get; init; }
        void SendPacket<T>(T packet) where T : IPacket<T>;
        void MsgBox(string msg);
        bool MsgBoxYesNo(string msg);
        void SynchronizeTickCount(int tickCount);
        void SynchronizeOtherPlayersName(List<string> otherPlayers);
        void SynchronizePlayerName(string name);
        void SynchronizeOtherPlayerFieldLitst(string name, FieldListRecord record);
        void AddOthersChatMessage(string senderName, string message);
        void AddSelfChatMessage(string receiver, string message);
    }
}
