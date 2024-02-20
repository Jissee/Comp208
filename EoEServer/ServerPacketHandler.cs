using EoE.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPacketHandler : PacketHandler
    {
        private Socket socket;
        public ServerPacketHandler(Socket socket)
        {
            this.socket = socket;
        }

        public override void ReceivePacket(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryReader br = new BinaryReader(stream);
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
                packet.Handle(new PacketContext { Distribution = EoE.Distribution.Server });
            }
            else
            {
                throw new Exception($"Cannot decode packet.");
            }
        }

        public override void SendPacket<T>(T packet)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
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

            socket.Send(data);

            //return data;
        }

        public override void SendPacket<T>(T packet)
        {
            throw new NotImplementedException();
        }
    }
}
