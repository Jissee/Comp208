using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Network.Packets.GonverancePacket
{
    public class PopulationUpdatePacket : IPacket<PopulationUpdatePacket>
    {
        private PopulationRecord populationRecord;

        public PopulationUpdatePacket(PopulationRecord populationRecord)
        {
            this.populationRecord = populationRecord;
        }
        public static PopulationUpdatePacket Decode(BinaryReader reader)
        {
            return new PopulationUpdatePacket(PopulationRecord.decoder(reader));
        }

        public static void Encode(PopulationUpdatePacket obj, BinaryWriter writer)
        {
            PopulationRecord.encoder(obj.populationRecord, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    if (client.GonveranceManager.PopManager is IClientPopManager popManager)
                    {
                        popManager.Synchronize(populationRecord);
                    }
                }
            }
        }
    }
}
