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
        protected Dictionary<string, Type> packetTypes;
        protected Dictionary<Type, Delegate> encoders = new Dictionary<Type, Delegate>();
        protected Dictionary<Type, Delegate> decoders = new Dictionary<Type, Delegate>();

        public PacketHandler()
        {
            packetTypes = new Dictionary<string, Type>();
        }

        public void Register<T>() where T : IPacket<T>
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
        public abstract void ReceivePacket(byte[] data);


        /// <summary>
        /// 把数据包对象编码成二进制数据，然后从网络发送数据
        /// </summary>
        /// <param name="packet"></param>
        public abstract void SendPacket<T>(T packet) where T : IPacket<T>;




    }
}
