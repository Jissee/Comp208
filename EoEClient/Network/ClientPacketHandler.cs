using EoE.Network;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EoE.Client.Network
{
    internal class ClientPacketHandler : PacketHandler
    {
        private Client client;

        public ClientPacketHandler(Client client)
        {
            this.client = client;
        }

        public override void ReceivePacket(byte[] data, PacketContext context)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryReader br = new BinaryReader(stream);
            bool isRedirected = br.ReadBoolean();
            if (isRedirected)
            {
                string redirectTarget = br.ReadString();
                if(redirectTarget != client.PlayerName)
                {
                    throw new Exception($"Illegal redirection, the packet of {redirectTarget} is received by {client.PlayerName}");
                }
            }
            string tp = br.ReadString();

            Type type = packetTypes[tp];

            Delegate decoder;
            try
            {
                decoder = decoders[type];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot find encoder for {tp}, it is not registered.");
                return;
            }

            IBasePacket packet = (IBasePacket)decoder.DynamicInvoke(br);
            if (packet != null)
            {
                packet.Handle(context);
            }
            else
            {
                throw new Exception($"Cannot decode packet.");
            }
        }

        public override void SendPacket<T>(T packet, Socket connection, IPlayer? redirectTarget)
        {
            
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            if(redirectTarget is RemotePlayer remote)
            {
                bw.Write(true);
                bw.Write(remote.PlayerName);
            }
            else
            {
                bw.Write(false);
            }

            Type packetType = packet.GetType();
            string packetTypeString = packetType.FullName;
            if (packetTypeString == null)
            {
                throw new Exception("Invalid packet type");
            }

            bw.Write(packetTypeString);

            Delegate encoder;
            try
            {
                encoder = encoders[packetType];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot find encoder for {packetTypeString}, it is not registered.");
                return;
            }
            encoder.DynamicInvoke(packet, bw);

            byte[] data = ms.ToArray();

            connection.Send(data);
        }
    }
}
