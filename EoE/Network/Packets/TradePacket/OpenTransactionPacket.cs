using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem;
using EoE.TradeSystem;

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
                                
                                break;
                            default:
                                throw new Exception("no such type");
                        }
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

