using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    internal class PacketI : IPacket<PacketI>
    {
        private int i;
        public PacketI(int i)
        {
            this.i = i;
        }

        public void Handle(PacketContext context)
        {
            Console.WriteLine($"Received PacketI {i}");
        }

        public static void Encode(PacketI obj, BinaryWriter writer)
        {
            writer.Write(obj.i);
        }

        public static PacketI Decode(BinaryReader reader)
        {
            return new PacketI(reader.ReadInt32());
        }
    }
}
