using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarDeclarationPacket : IPacket<WarDeclarationPacket>
    {

        public WarDeclarationPacket() 
        { 

        }
        public static WarDeclarationPacket Decode(BinaryReader reader)
        {
        }

        public static void Encode(WarDeclarationPacket obj, BinaryWriter writer)
        {
        }

        public void Handle(PacketContext context)
        {
        }
    }
}
