using EoE.GovernanceSystem;
using EoE.Network.Packets;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.TradeSystem
{
    public class GameTransaction
    {
        public string Offeror { get; init; }
        public Guid Id { get; init; }
        public bool IsOpen { get; init; }
        public string? Recipient { get; init; }
        public ResourceStack OfferorOffer { get; set; }
        public ResourceStack RecipientOffer { get; set; }


        public GameTransaction(string offeror,Guid id, ResourceStack offerorOffer, ResourceStack recipientOffer,bool isOpen,string? recipient) 
        {
            this.Offeror = offeror;
            this.Id = id;
            OfferorOffer = offerorOffer;
            RecipientOffer = recipientOffer;
            Recipient = recipient;
            IsOpen = isOpen;
            Recipient = recipient;
        }

        public static Encoder<GameTransaction> encoder = (GameTransaction obj, BinaryWriter writer) =>
        {
            writer.Write(obj.Offeror);
            writer.Write(obj.Id.ToByteArray());
            writer.Write(obj.IsOpen);
            if (!obj.IsOpen)
            {
                writer.Write(obj.Recipient);
            }
            ResourceStack.encoder(obj.OfferorOffer,writer);
            ResourceStack.encoder(obj.RecipientOffer, writer);
        };

        public static Decoder<GameTransaction> decoder = (BinaryReader reader) =>
        {
            string offer = reader.ReadString();
            byte[] bytes = reader.ReadBytes(16);
            Guid id = new Guid(bytes);
            bool isOpen = reader.ReadBoolean();
            string recipient = null;
            if (!isOpen)
            {
                recipient = reader.ReadString();
            }
            ResourceStack offerorOffer = ResourceStack.decoder(reader);
            ResourceStack recipientOffer = ResourceStack.decoder(reader);

            GameTransaction transaction;
            if (isOpen)
            {
                transaction = new GameTransaction(offer, id, offerorOffer, recipientOffer, isOpen, recipient);
            }
            else
            {
                transaction = new GameTransaction(offer, id, offerorOffer, recipientOffer, isOpen, recipient);
            }
            return transaction;
        };

    }
}
