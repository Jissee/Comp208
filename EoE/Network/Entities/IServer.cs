using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets;
using System.Net.Sockets;

namespace EoE.Network.Entities
{
    public interface IServer : INetworkEntity
    {
        Socket ServerSocket { get; }
        PacketHandler PacketHandler { get; }
        GameStatus Status { get; }
        bool IsGameRunning { get; }
        static void Log(string type, string message)
        {
            Console.WriteLine($"[{DateTime.Now}] [{type}] {message}");
        }
        static void Log(string type, string message, Exception e)
        {
            Console.WriteLine($"[{DateTime.Now}] [{type}] {message}\n {e}");
        }
        IServerPlayerList PlayerList { get; }
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
        void GameSummary();


    }
}
