using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public class PacketContext
    {
        public PacketContext(NetworkDirection direction, IPlayer? playerSender, INetworkEntity? receiver) 
        { 
            NetworkDirection = direction;
            PlayerSender = playerSender;
            Receiver = receiver;
        }
        public NetworkDirection NetworkDirection { get; }
        public IPlayer? PlayerSender { get; }
        public INetworkEntity? Receiver { get; }
    }
}
