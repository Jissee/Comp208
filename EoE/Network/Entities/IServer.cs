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
using EoE.Events;

namespace EoE.Network.Entities
{
    public interface IServer : INetworkEntity
    {
        Socket ServerSocket { get; }
        PacketHandler PacketHandler { get; }
        EventList EventList { get; }
        GameStatus Status { get; }
        static void Log(string type, string message) 
        {
            Console.WriteLine($"[{DateTime.Now}] [{type}] {message}");
        }
        public IServerPlayerList PlayerList { get; }
        void InitPlayerName(IPlayer player, string name);
        void Start();
        void Stop();
        bool IsNeedRestart();
        void Restart();
        void Boardcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
        IPlayer? GetPlayer(string playerName);
        void CheckPlayerTickStatus();
        void BeginGame();
        void SetGame(int playerCount, int totalTick);


    }
}
