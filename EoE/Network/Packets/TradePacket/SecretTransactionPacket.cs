using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction = EoE.TradeSystem.Transaction;

namespace EoE.Network.Packets.TradePacket
{
    public class SecretTransactionPacket : IPacket<SecretTransactionPacket>
    {
        private SecretTransactionOperation operation;
        private Transaction transaction;
        public SecretTransactionPacket(SecretTransactionOperation operation, Transaction transaction)
        {
            this.operation = operation;
            this.transaction = transaction;
        }
        public static SecretTransactionPacket Decode(BinaryReader reader)
        {
            return new SecretTransactionPacket((SecretTransactionOperation)reader.ReadInt32(), Transaction.decoder(reader));
        }

        public static void Encode(SecretTransactionPacket obj, BinaryWriter writer)
        {
            writer.Write((int)obj.operation);
            Transaction.encoder(obj.transaction, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    if (transaction.IsOpen)
                    {
                        switch (operation)
                        {
                            case SecretTransactionOperation.Creat:
                                server.TradeHandler.CreatSecretTransaction(transaction);
                                break;
                            case SecretTransactionOperation.Accept:
                                server.TradeHandler.AcceptSecretTransaction(transaction);
                                break;
                            default:
                                throw new Exception("no such type");
                        }
                    }
                    else
                    {
                        throw new Exception("wrong call, not open");
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
                            //Todo
                            break;
                        case SecretTransactionOperation.Accept:
                            //Todo
                            break;
                        default:
                            throw new Exception("no such type");
                    }
                }
            }
        }
    }
    public enum SecretTransactionOperation
    {
        Creat,
        Accept
    }
}
