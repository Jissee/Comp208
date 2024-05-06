using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.Network.Entities;
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
                                server.PlayerList.TradeManager.CreatSecretTransaction(transaction);
                                break;
                            case SecretTransactionOperation.Accept:
                                server.PlayerList.TradeManager.AcceptSecretTransaction(transaction);
                                break;
                            case SecretTransactionOperation.Reject:
                                server.PlayerList.TradeManager.RejectSecretTransaction(transaction,context.PlayerSender.PlayerName);
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
                            string message = $"Player : {transaction.Offeror} send an trade requirement to you\n" +
                                $"{transaction.Offeror} offer: \n" +
                                transaction.OfferorOffer[0].ToString() + "\n" +
                                transaction.OfferorOffer[1].ToString() + "\n" +
                                transaction.OfferorOffer[2].ToString() + "\n" +
                                transaction.OfferorOffer[3].ToString() + "\n" +
                                transaction.OfferorOffer[4].ToString() + "\n" +
                                transaction.OfferorOffer[5].ToString() + "\n"+
                                "You offer: \n" +
                                transaction.RecipientOffer[0].ToString() + "\n" +
                                transaction.RecipientOffer[1].ToString() + "\n" +
                                transaction.RecipientOffer[2].ToString() + "\n" +
                                transaction.RecipientOffer[3].ToString() + "\n" +
                                transaction.RecipientOffer[4].ToString() + "\n" +
                                transaction.RecipientOffer[5].ToString() + "\n";
                            if (player.MsgBoxYesNo(message))
                            {
                                player.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Accept, transaction));
                            }
                            else
                            {
                                player.SendPacket(new SecretTransactionPacket(SecretTransactionOperation.Reject, transaction));
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
