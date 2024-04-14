using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarDeclarationPacket : IPacket<WarDeclarationPacket>
    {

        private string warName;
        public WarDeclarationPacket() 
        { 

        }
        public static WarDeclarationPacket Decode(BinaryReader reader)
        {
            return new WarDeclarationPacket();
        }

        public static void Encode(WarDeclarationPacket obj, BinaryWriter writer)
        {
        }

        public void Handle(PacketContext context)
        {
        }
    }
}
