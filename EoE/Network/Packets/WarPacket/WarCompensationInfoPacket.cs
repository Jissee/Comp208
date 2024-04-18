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
        private string warName;
        private ResourceListRecord record;
        private int pop;
        private int field;
        private string compensator;
        public WarCompensationInfoPacket(string warName, ResourceListRecord record, int pop, int field, string compensator) 
        { 
            this.warName = warName;
            this.record = record;
            this.pop = pop;
            this.field = field;
            this.compensator = compensator;
        }
        public static WarCompensationInfoPacket Decode(BinaryReader reader)
        {
            return new WarCompensationInfoPacket(reader.ReadString(), ResourceListRecord.decoder(reader),
                                                reader.ReadInt32(), reader.ReadInt32(),
                                                reader.ReadString());
        }

        public static void Encode(WarCompensationInfoPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            ResourceListRecord.encoder(obj.record, writer);
            writer.Write(obj.pop);
            writer.Write(obj.field);
            writer.Write(obj.compensator);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Server2Client)
            {
                IClient client = (IClient)context.Receiver;
                client.MsgBox($"""
                    You will get these from {compensator} in the war "{warName}"!
                    Population: {pop}
                    Fields: {field}
                    Silion: {record.siliconCount}
                    Copper: {record.copperCount}
                    Iron: {record.ironCount}
                    Aluminum: {record.aluminumCount}
                    Electronic: {record.electronicCount}
                    Industrial: {record.industrialCount}
                    """);
            }
        }
    }
}
