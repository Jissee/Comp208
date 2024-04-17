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
        private string playerNmae;

        public OtherPlayerFieldUpdate(FieldListRecord playerFieldList,string playerName)
        {
            this.playerFieldList = playerFieldList;
            this.playerNmae = playerName;
        }
        public static OtherPlayerFieldUpdate Decode(BinaryReader reader)
        {
            return new OtherPlayerFieldUpdate(FieldListRecord.decoder(reader),reader.ReadString());
        }

        public static void Encode(OtherPlayerFieldUpdate obj, BinaryWriter writer)
        {
            FieldListRecord.encoder(obj.playerFieldList, writer);
            writer.Write(obj.playerNmae);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.SynchronizeOtherPlayerFieldLitst(playerNmae, playerFieldList);
                }
            }
        }
    }
}
