using EoE.Governance;
using EoE.Network.Entities;

namespace EoE.Network.Packets.GonverancePacket
{
    public class FieldConvertPacket : IPacket<FieldConvertPacket>
    {
        FieldStack origin;
        FieldStack converted;

        public FieldConvertPacket(FieldStack origin, FieldStack converted)
        {
            this.origin = origin;
            this.converted = converted;
        }
        public static FieldConvertPacket Decode(BinaryReader reader)
        {
            return new FieldConvertPacket(FieldStack.decoder(reader), FieldStack.decoder(reader));
        }

        public static void Encode(FieldConvertPacket obj, BinaryWriter writer)
        {
            FieldStack.encoder(obj.origin, writer);
            FieldStack.encoder(obj.converted, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer player = context.PlayerSender;
                    player.GonveranceManager.FieldList.FieldConversion(origin, converted);
                }
            }
        }
    }
}
