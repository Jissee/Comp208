using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket
{
     
    public class OtherPlayerFieldUpdate : IPacket<OtherPlayerFieldUpdate>
    {
        private FieldListRecord playerFieldList;

        public OtherPlayerFieldUpdate(FieldListRecord playerFieldList)
        {
            this.playerFieldList = playerFieldList;
        }
        public static OtherPlayerFieldUpdate Decode(BinaryReader reader)
        {
            return new OtherPlayerFieldUpdate(FieldListRecord.decoder(reader));
        }

        public static void Encode(OtherPlayerFieldUpdate obj, BinaryWriter writer)
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
                    client.SynchronizeOtherPlayerFieldLitst(context.PlayerSender.PlayerName, playerFieldList);
                }
            }
        }
    }
}
