using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.TradePacket;
using EoE.Network;
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
            Register<NewPacket>();
            Register<ResourceUpdatePacket>();
            Register<FieldUpdatePacket>();
            Register<ResourceUpdatePacket>();
            Register<PopulationUpdatePacket>();
            Register<OpenTransactionPacket>();
            Register<SecretTransactionPacket>();
            Register<SetPopAllocationPacket>();
            Register<SetExplorationPacket>();
            Register<ServerMessagePacket>();
            Register<OpenTransactionSynchronizePacket>();
            Register<GameSettingPacket>();
            Register<FieldBoardCastPacket>();
            Register<EnterRoomPacket>();
            Register<EnterGamePacket>();
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
        /// <param invitorName="data"></param>
        public abstract void ReceivePacket(byte[] data, PacketContext context, string fromName);


        /// <summary>
        /// 把数据包对象编码成二进制数据，然后从网络发送数据
        /// </summary>
        /// <param invitorName="packet"></param>
        public abstract void SendPacket<T>(T packet, Socket connection, string targetName) where T : IPacket<T>;




    }
}
