using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Network
{
    public class ServerPacketHandler : PacketHandler
    {
        private Server server;
        public ServerPacketHandler(Server server)
        {
            this.server = server;
        }

        public override void ReceivePacket(byte[] data, PacketContext context, string fromName)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryReader br = new BinaryReader(stream);

            string tp = br.ReadString();

            Type type = packetTypes[tp];
            IServer.Log("Packet Info", $"Receiving packet {tp} from {fromName}");

            Delegate decoder;
            try
            {
                decoder = decoders[type];
            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot find encoder for {tp}, it is not registered.");
                return;
            }

            IBasePacket packet = (IBasePacket)decoder.DynamicInvoke(br);
            if (packet != null)
            {
                packet.Handle(context);
            }
            else
            {
                IServer.Log("Packet Error", $"Cannot handle packet {tp}");
            }
        }

        public override void SendPacket<T>(T packet, Socket connection, string playerName)
        {
            
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(0L);
            Type packetType = packet.GetType();
            string packetTypeString = packetType.FullName;
            IServer.Log("Packet Info", $"Sending packet {packetTypeString} to {playerName}.");
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
                IServer.Log("Packet Error", $"Cannot find encoder for {packetTypeString}, it is not registered.");
                return;
            }
            encoder.DynamicInvoke(packet, bw);

            long length = ms.Position - 8;
            bw.Seek(0, SeekOrigin.Begin);
            bw.Write(length);

            byte[] data = ms.ToArray();


            connection.Send(data);

            //return data;
        }
    }
}
