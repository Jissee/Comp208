using EoE.Network.Entities;
using EoE.TradeSystem;

namespace EoE.Network.Packets.TradePacket
{
    public class OpenTransactionSynchronizePacket : IPacket<OpenTransactionSynchronizePacket>
    {
        private int transactionCount;
        private List<GameTransaction> openOrders;

        public OpenTransactionSynchronizePacket(int transactionCount, List<GameTransaction> openOrders)
        {
            this.transactionCount = transactionCount;
            this.openOrders = openOrders;
        }
        public static OpenTransactionSynchronizePacket Decode(BinaryReader reader)
        {
            List<GameTransaction> openOrders = new List<GameTransaction>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                openOrders.Add(GameTransaction.decoder(reader));
            }
            return new OpenTransactionSynchronizePacket(count, openOrders);
        }

        public static void Encode(OpenTransactionSynchronizePacket obj, BinaryWriter writer)
        {
            writer.Write(obj.transactionCount);
            for (int i = 0; i < obj.transactionCount; i++)
            {
                GameTransaction.encoder(obj.openOrders[i], writer);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.TradeManager.Synchronize(openOrders);
                }
            }
        }
    }
}
