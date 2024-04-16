using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class EnterRoomPacket : IPacket<EnterRoomPacket>
    {
        private bool isFirst;

        public EnterRoomPacket(bool isFirst)
        {
            this.isFirst = isFirst;
        }
        public static EnterRoomPacket Decode(BinaryReader reader)
        {
            return new EnterRoomPacket(reader.ReadBoolean());
        }

        public static void Encode(EnterRoomPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.isFirst);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    if (isFirst)
                    {
                        client.WindowManager.ShowGameSettingWindow();
                    }
                    else
                    {
                        client.WindowManager.ShowGameEntterWindow();
                    }
                }
            }
        }
    }
}
