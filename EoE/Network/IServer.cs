using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EoE.TradeSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Treaty;
using EoE.WarSystem.Interface;

namespace EoE.Network
{
    public interface IServer : INetworkEntity
    {
        Socket ServerSocket { get; }
        public ITradeManager TradeHandler { get; }
        public IServerPlayerList PlayerList { get; }
        void InitPlayerName(IPlayer player, string name);
        void Start();
        void Stop();
        void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
        IPlayer? GetPlayer(string playerName);
        void CheckPlayerTickStatus();

        List<IPlayer> GetProtectorsRecursively(IPlayer target);
        
    }
}
