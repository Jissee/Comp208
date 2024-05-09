using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Trade;
using EoE.Treaty;
using EoE.War.Interface;

namespace EoE.Governance.ServerInterface
{
    /// <summary>
    /// Managing all players including connection and disconnection.
    /// </summary>
    public interface IServerPlayerList : ITickable
    {
        public int PlayerCount { get; }
        IPlayer? Host { get; }
        ITreatyManager TreatyManager { get; }
        IWarManager WarManager { get; }
        IServerTradeManager TradeManager { get; }
        void GameBegin();
        bool AllBegins { get; }
        public List<IPlayer> Players { get; }
        void PlayerLogin(IPlayer player);
        void PlayerLogout(IPlayer player);
        void HandlePlayerDisconnection();
        void HandlePlayerMessage(PacketHandler packetHandler, IServer server);
        bool CheckPlayerTickStatus();
        void InitPlayerName(IPlayer playerRef, string name);
        void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
        IPlayer? GetPlayer(string name);
        void SetPlayerCount(int playerCount);
    }
}
