using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    public class WarIntensionPacket : IPacket<WarIntensionPacket>
    {
        private string targetPlayerName;
        public WarIntensionPacket(string targetPlayerName)
        {
            this.targetPlayerName = targetPlayerName;
        }
        public static WarIntensionPacket Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public static void Encode(WarIntensionPacket obj, BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Handle(PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}
