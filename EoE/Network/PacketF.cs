using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    internal class PacketF : IPacket<PacketF>
    {
        private float f;
        public PacketF(float f)
        {
            this.f = f;
        }

        public static void Encode(PacketF packet, BinaryWriter writer)
        {
            writer.Write(packet.f);
        }
        public static PacketF Decode(BinaryReader reader)
        {
            return new PacketF(reader.ReadSingle());
        }
        public void Handle(PacketContext context)
        {
            Console.WriteLine($"Received PacketF {f}");
        }
    }
}
