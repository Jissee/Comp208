using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using EoE.GovernanceSystem;
using EoE.TradeSystem;
using Transaction = EoE.TradeSystem.Transaction;

namespace EoE.Network.Packets.TradePacket
{
    public class OpenTransactionPacket : IPacket<OpenTransactionPacket>
    {
        private OpenTransactionOperation operation;
        private Transaction transaction;

        public OpenTransactionPacket(OpenTransactionOperation operation, Transaction transaction)
        {
            this.operation = operation;
            this.transaction = transaction;
        }
        public static OpenTransactionPacket Decode(BinaryReader reader)
        {
            return new OpenTransactionPacket( (OpenTransactionOperation)reader.ReadInt32(),Transaction.decoder(reader));
        }

        public static void Encode(OpenTransactionPacket obj, BinaryWriter writer)
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
                            case OpenTransactionOperation.Creat:
                                server.TradeHandler.CreatOponTransaction(transaction);
                                break;
                            case OpenTransactionOperation.Accept:
                                server.TradeHandler.AcceptOpenTransaction(transaction.Id, context.PlayerSender.PlayerName);
                                break;
                            case OpenTransactionOperation.Cancel:
                                server.TradeHandler.CancelOpenTransaction(transaction.Id, context.PlayerSender.PlayerName);
                                break;
                            case OpenTransactionOperation.Alter:
                                server.TradeHandler.AlterOpenTransaction(transaction.Id, transaction.OfferorOffer, transaction.RecipientOffer);
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
                IPlayer ne = context.PlayerSender!;
                if (ne is IPlayer player)
                {
                    switch (operation)
                    {
                        case OpenTransactionOperation.Creat:
                            //Todo
                            break;
                        case OpenTransactionOperation.Accept:
                            //Todo
                            break;
                        case OpenTransactionOperation.Cancel:
                            //Todo
                            break;
                        case OpenTransactionOperation.Alter:
                            //Todo
                            break;
                        default:
                            throw new Exception("no such type");
                    }
                }
            }
        }
    }

    public enum OpenTransactionOperation
    {
        Creat,
        Accept,
        Cancel,
        Alter

    }
}

