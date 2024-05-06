using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using System.Net.Sockets;

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
                IServer.Log("Packet Error", $"Cannot find encoder for {tp}, it is not registered.", ex);
                return;
            }

            IBasePacket packet;

            try
            {
                packet = (IBasePacket)decoder.DynamicInvoke(br);
            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot decode packet {tp}.", ex);
                return;
            }

            try
            {
                packet.Handle(context);
            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot handle packet {tp}.", ex);
            }
        }

        public override void SendPacket<T>(T packet, Socket connection, string playerName)
        {

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(0L);
            Type packetType = packet.GetType();
            string tp = packetType.FullName;
            IServer.Log("Packet Info", $"Sending packet {tp} to {playerName}.");
            if (tp == null)
            {
                throw new Exception("Invalid packet type");
            }

            bw.Write(tp);

            Delegate encoder;
            try
            {
                encoder = encoders[packetType];

            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot find encoder for {tp}, it is not registered.", ex);
                return;
            }

            try
            {
                encoder.DynamicInvoke(packet, bw);
            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot decode packet {tp}.", ex);
                return;
            }


            long length = ms.Position - 8;
            bw.Seek(0, SeekOrigin.Begin);
            bw.Write(length);

            byte[] data = ms.ToArray();

            try
            {
                connection.Send(data);
            }
            catch (Exception ex)
            {
                IServer.Log("Packet Error", $"Cannot send packet {tp}.", ex);
            }


            //return data;
        }
    }
}
