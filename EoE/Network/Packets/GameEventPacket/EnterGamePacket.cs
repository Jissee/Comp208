using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class EnterGamePacket : IPacket<EnterGamePacket>
    {
        
        public static EnterGamePacket Decode(BinaryReader reader)
        {
            return new EnterGamePacket();
        }

        public static void Encode(EnterGamePacket obj, BinaryWriter writer)
        {
            
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    

                }
            }
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    
                }
            }
        }
    }
}
