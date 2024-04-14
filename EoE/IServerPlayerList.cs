using EoE.Network.Entities;
using EoE.Network;
using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.Network.Packets;

namespace EoE
{
    public interface IServerPlayerList: ITickable
    {
        ITreatyManager TreatyManager { get; }
        IWarManager WarManager { get; }
        public List<IPlayer> Players { get; }
        void PlayerLogin(IPlayer player);
        void HandlePlayerDisconnection();
        void HandlePlayerMessage(PacketHandler packetHandler, IServer server);
        bool CheckPlayerTickStatus();
        void InitPlayerName(IPlayer playerRef, string name);
        void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
        IPlayer? GetPlayer(string name);
    }
}
