using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Network.Packets.GonverancePacket
{
    public class SetPopAllocationPacket : IPacket<SetPopAllocationPacket>
    {
        private PopulationRecord record;

        public SetPopAllocationPacket(
            PopulationRecord record
            )
        {
            this.record = record;
        }
        public static SetPopAllocationPacket Decode(BinaryReader reader)
        {
            return new SetPopAllocationPacket(PopulationRecord.decoder(reader));
        }

        public static void Encode(SetPopAllocationPacket obj, BinaryWriter writer)
        {
            PopulationRecord.encoder(obj.record, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer player = context.PlayerSender;
                    player.GonveranceManager.PopManager.SetAllocation(record);
                }
            }
        }
    }
}
