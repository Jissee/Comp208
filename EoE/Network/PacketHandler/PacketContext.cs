using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using EoE.Network.Entities;

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
        /// <summary>
        /// 若接收方为服务端，则为发送的客户端
        /// </summary>
        public IPlayer? PlayerSender { get; }
        /// <summary>
        /// 信息接收方，为服务端或客户端
        /// </summary>
        public INetworkEntity Receiver { get; }
    }
}
