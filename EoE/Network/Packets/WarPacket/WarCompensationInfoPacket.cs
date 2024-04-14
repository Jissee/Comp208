using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarCompensationInfoPacket : IPacket<WarCompensationInfoPacket>
    {
        private ResourceListRecord record;
        private int pop;
        private int field;
        private string compensator;
        public WarCompensationInfoPacket(ResourceListRecord record, int pop, int field, string compensator) 
        { 
            this.record = record;
            this.pop = pop;
            this.field = field;
            this.compensator = compensator;
        }
        public static WarCompensationInfoPacket Decode(BinaryReader reader)
        {
            return new WarCompensationInfoPacket(ResourceListRecord.decoder(reader),
                                                reader.ReadInt32(), reader.ReadInt32(),
                                                reader.ReadString());
        }

        public static void Encode(WarCompensationInfoPacket obj, BinaryWriter writer)
        {
            ResourceListRecord.encoder(obj.record, writer);
            writer.Write(obj.pop);
            writer.Write(obj.field);
            writer.Write(obj.compensator);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Server2Client)
            {
                //todo:show players their compensator and corresponding compensation
            }
        }
    }
}
