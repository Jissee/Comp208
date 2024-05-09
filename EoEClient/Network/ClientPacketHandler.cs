using EoE.Network;
using EoE.Network.Packets;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Windows;

namespace EoE.Client.Network
{
    internal class ClientPacketHandler : PacketHandler
    {


        public ClientPacketHandler()
        {

        }

        public override void ReceivePacket(byte[] data, PacketContext context, string fromName)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryReader br = new BinaryReader(stream);
            string tp = br.ReadString();

            Debug.WriteLine(tp);
            Type type = packetTypes[tp];

            Delegate decoder;
            try
            {
                decoder = decoders[type];
            }
            catch (Exception ex)
            {
                Client.ShowException("Packet", $"Cannot find encoder for {tp}, it is not registered.", ex);
                return;
            }
            IBasePacket packet;
            try
            {
                packet = (IBasePacket)decoder.DynamicInvoke(br);
            }
            catch (Exception ex)
            {
                Client.ShowException("Packet", $"Cannot decode packet {tp}.", ex);
                return;
            }

            try
            {
                packet.Handle(context);
            }
            catch (Exception ex)
            {
                Client.ShowException("Packet", $"Cannot handle packet {tp}.", ex);
            }
        }

        public override void SendPacket<T>(T packet, Socket connection, string targetName)
        {

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(0L);

            Type packetType = packet.GetType();
            string tp = packetType.FullName;
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
                Client.ShowException("Packet", $"Cannot find encoder for {tp}, it is not registered.", ex);
                return;
            }
            try
            {
                encoder.DynamicInvoke(packet, bw);
            }
            catch (Exception ex)
            {
                Client.ShowException("Packet", $"Cannot encode packet {tp}.", ex);
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
                Client.ShowException("Packet", $"Cannot send packet {tp}. It seems that you lost the game.", ex);
            }
        }
    }
}
