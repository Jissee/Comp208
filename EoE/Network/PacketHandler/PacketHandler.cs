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
using EoE.Network.Packets.WarPacket;
using EoE.Network.Packets.TreatyPacket;
using EoE.Network.Packets.Chat;

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
            Register<EnterRoomPacket>();
            Register<EnterGamePacket>();
            Register<PlayerListPacket>();
            Register<FinishTickPacket>();
            Register<SyntheticArmyPacket>();
            Register<OtherPlayerFieldUpdate>();
            Register<FieldConvertPacket>();
            Register<ChatPacket>();

            Register<FillInFrontierPacket>();
            Register<WarCompensationInfoPacket>();
            Register<WarDeclarablePacket>();
            Register<WarDeclarationPacket>();
            Register<WarInformationPacket>();
            Register<WarIntensionPacket>();
            Register<WarInvitationPacket>();
            Register<WarInvitedPacket>();
            Register<WarNameQueryPacket>();
            Register<WarNameQueryRelatedPacket>();
            Register<WarQueryTargetPacket>();
            Register<WarSurrenderPacket>();
            Register<BreakTreatyPacket>();
            Register<ConfirmCommonDefenseTreatyPacket>();
            Register<ConfirmProtectiveTreatyPacket>();
            Register<NewCommonDefenseTreatyPacket>();
            Register<NewProtectiveTreatyPacket>();
            Register<QueryTreatyPacket>();
            Register<WarWidthQueryPacket>();
            Register<GameSummaryPacket>();
            Register<WarTargetPacket>();
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
        /// The binary data received from the network is recovered into a packet, and then the packet is processed
        /// </summary>
        /// <param invitorName="data"></param>
        public abstract void ReceivePacket(byte[] data, PacketContext context, string fromName);


        /// <summary>
        /// The packet object is encoded into binary data, and then the data is sent from the network
        /// </summary>
        /// <param invitorName="packet"></param>
        public abstract void SendPacket<T>(T packet, Socket connection, string targetName) where T : IPacket<T>;




    }
}
