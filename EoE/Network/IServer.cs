using EoE.Network.Packets;
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
        Socket ServerSocket { get; }
        List<IPlayer> Clients { get; }
        void Start();
        void Stop();
        void Broadcast<T>(T packet, Predicate<IPlayer> condition) where T : IPacket<T>;
        IPlayer? GetPlayer(string playerName);
        
    }
}
