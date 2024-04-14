using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class FirstPlayerPacket : IPacket<FirstPlayerPacket>
    {
        private bool isFirst;

        public FirstPlayerPacket(bool isFirst)
        {
            this.isFirst = isFirst;
        }
        public static FirstPlayerPacket Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public static void Encode(FirstPlayerPacket obj, BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Handle(PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}
