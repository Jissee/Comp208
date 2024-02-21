using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public interface IServer : INetworkEntity
    {
        public Socket ServerSocket { get; }
        public List<IPlayer> Clients { get; }
        void Start();
        void Stop();
        void SendPacket<T>(T packet, string playerName) where T : IPacket<T>;
        void SendPacket<T>(T packet, IPlayer player) where T : IPacket<T>;
        void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
    }
}
