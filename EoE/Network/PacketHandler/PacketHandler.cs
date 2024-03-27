using EoE.Network.Packets;
using EoE.Server.Network;
using EoE.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public abstract class PacketHandler
    {
        protected static Dictionary<string, Type> packetTypes;
        protected static Dictionary<Type, Delegate> encoders = new Dictionary<Type, Delegate>();
        protected static Dictionary<Type, Delegate> decoders = new Dictionary<Type, Delegate>();

        static PacketHandler()
        {
            packetTypes = new Dictionary<string, Type>();
            Register<PlayerLoginPacket>();
            Register<RemotePlayerSyncPacket>();
            Register<NewPacket>();
            Register<ResurceUpdatePacket>();
        }

        public static void Register<T>() where T : IPacket<T>
        {
            Type type = typeof(T);
            string packetTypeString = type.FullName;
            if(packetTypeString == null)
            {
                throw new Exception("Invalid packet type");
            }

            packetTypes[packetTypeString] = type;

            encoders[type] = T.Encode;
            decoders[type] = T.Decode;
        }
        /// <summary>
        /// 把从网络接收的二进制数据恢复成数据包，然后处理数据包
        /// </summary>
        /// <param name="data"></param>
        public abstract void ReceivePacket(byte[] data, PacketContext context);


        /// <summary>
        /// 把数据包对象编码成二进制数据，然后从网络发送数据
        /// </summary>
        /// <param name="packet"></param>
        public abstract void SendPacket<T>(T packet, Socket connection, IPlayer redirectTarget) where T : IPacket<T>;




    }
}
