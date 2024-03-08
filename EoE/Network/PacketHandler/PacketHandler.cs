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
            Register<ClientLoginPacket>();
            Register<NewPacket>();
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
        /// 准备数据包，将其编码成二进制数据，以便发送数据
        /// </summary>
        /// <param name="packet"></param>
        public abstract byte[] PreparePacket<T>(T packet) where T : IPacket<T>;




    }
}
