using EoE.Client;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class RoomOwnerPacket : IPacket<RoomOwnerPacket>
    {
        private bool isFirst;

        public RoomOwnerPacket(bool isFirst)
        {
            this.isFirst = isFirst;
        }
        public static RoomOwnerPacket Decode(BinaryReader reader)
        {
            return new RoomOwnerPacket(reader.ReadBoolean());
        }

        public static void Encode(RoomOwnerPacket obj, BinaryWriter writer)
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
                }
            }
        }
    }
}
