﻿using EoE.Network.Entities;
using GameTransaction = EoE.Trade.GameTransaction;

namespace EoE.Network.Packets.TradePacket
{
    public class OpenTransactionPacket : IPacket<OpenTransactionPacket>
    {
        private OpenTransactionOperation operation;
        private GameTransaction transaction;

        public OpenTransactionPacket(OpenTransactionOperation operation, GameTransaction transaction)
        {
            this.operation = operation;
            this.transaction = transaction;
        }
        public static OpenTransactionPacket Decode(BinaryReader reader)
        {
            return new OpenTransactionPacket((OpenTransactionOperation)reader.ReadInt32(), GameTransaction.decoder(reader));
        }

        public static void Encode(OpenTransactionPacket obj, BinaryWriter writer)
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
                    if (transaction.IsOpen)
                    {
                        switch (operation)
                        {
                            case OpenTransactionOperation.Create:
                                server.PlayerList.TradeManager.CreateOpenTransaction(transaction);
                                break;
                            case OpenTransactionOperation.Accept:
                                server.PlayerList.TradeManager.AcceptOpenTransaction(transaction.Id, context.PlayerSender!.PlayerName);
                                break;
                            case OpenTransactionOperation.Cancel:
                                server.PlayerList.TradeManager.CancelOpenTransaction(transaction.Id, context.PlayerSender!.PlayerName);
                                break;
                            case OpenTransactionOperation.Alter:
                                server.PlayerList.TradeManager.AlterOpenTransaction(transaction.Id, transaction.OfferorOffer, transaction.RecipientOffer, context.PlayerSender!.PlayerName);
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
                if (ne is IClient client)
                {
                    switch (operation)
                    {
                        case OpenTransactionOperation.Create:
                            client.TradeManager.AddOpenTransaction(transaction);
                            break;
                        case OpenTransactionOperation.Accept:
                            client.TradeManager.RemoveOpenTransaction(transaction);
                            break;
                        case OpenTransactionOperation.Cancel:
                            client.TradeManager.RemoveOpenTransaction(transaction);
                            break;
                        case OpenTransactionOperation.Alter:
                            client.TradeManager.RemoveOpenTransaction(transaction);
                            client.TradeManager.AddOpenTransaction(transaction);
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
        Create,
        Accept,
        Cancel,
        Alter

    }
}

