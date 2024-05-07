using EoE.Network.Entities;
using System.Text;
using GameTransaction = EoE.TradeSystem.GameTransaction;

namespace EoE.Network.Packets.TradePacket
{
    public class SecretTransactionPacket : IPacket<SecretTransactionPacket>
    {
        private SecretTransactionOperation operation;
        private GameTransaction transaction;
        public SecretTransactionPacket(SecretTransactionOperation operation, GameTransaction transaction)
        {
            this.operation = operation;
            this.transaction = transaction;
        }
        public static SecretTransactionPacket Decode(BinaryReader reader)
        {
            return new SecretTransactionPacket((SecretTransactionOperation)reader.ReadInt32(), GameTransaction.decoder(reader));
        }

        public static void Encode(SecretTransactionPacket obj, BinaryWriter writer)
        {
            writer.Write((int)obj.operation);
            GameTransaction.encoder(obj.transaction, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    if (!transaction.IsOpen)
                    {
                        switch (operation)
                        {
                            case SecretTransactionOperation.Creat:
                                server.PlayerList.TradeManager.CreateSecretTransaction(transaction);
                                break;
                            case SecretTransactionOperation.Accept:
                                server.PlayerList.TradeManager.AcceptSecretTransaction(transaction);
                                break;
                            case SecretTransactionOperation.Reject:
                                server.PlayerList.TradeManager.RejectSecretTransaction(transaction, context.PlayerSender.PlayerName);
                                break;
                            default:
                                throw new Exception("no such type");
                        }
                    }
                    else
                    {
                        throw new Exception("wrong call, is open");
                    }
                }
            }
            else
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient player)
                {
                    switch (operation)
                    {
                        case SecretTransactionOperation.Creat:
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine($"Player : {transaction.Offeror} send an trade requirement to you");
                            sb.AppendLine($"{transaction.Offeror} offer:");

                            foreach (var item in transaction.OfferorOffer)
                            {
                                if (item.Count > 0)
                                {
                                    sb.AppendLine(item.ToString());
                                }
                            }
                            sb.AppendLine("You offer:");
                            foreach (var item in transaction.RecipientOffer)
                            {
                                if (item.Count > 0)
                                {
                                    sb.AppendLine(item.ToString());
                                }
                            }
                            if (player.MsgBoxYesNo(sb.ToString()))
                            {
                                player.TradeManager.AcceptSecretTransaction(transaction);
                            }
                            else
                            {
                                player.TradeManager.RejectSecretTransaction(transaction);
                            }
                            break;
                        default:
                            throw new Exception("wrong call");
                    }
                }
            }
        }
    }
    public enum SecretTransactionOperation
    {
        Creat,
        Accept,
        Reject
    }
}
