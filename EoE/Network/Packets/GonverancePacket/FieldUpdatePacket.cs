using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket
{
    public class FieldUpdatePacket : IPacket<FieldUpdatePacket>
    {
        private FieldListRecord playerFieldList;

        public FieldUpdatePacket(FieldListRecord playerFieldList)
        {
            this.playerFieldList = playerFieldList;
        }
        public static FieldUpdatePacket Decode(BinaryReader reader)
        {
            return new FieldUpdatePacket(FieldListRecord.decoder(reader));
        }

        public static void Encode(FieldUpdatePacket obj, BinaryWriter writer)
        {
            FieldListRecord.encoder(obj.playerFieldList, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                   if( client.GonveranceHandler.FieldList is IClientFieldList fieldList)
                    {
                        fieldList.Synchronization(playerFieldList);
                    }
                }
            }
        }
    }
}
