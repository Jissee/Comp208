﻿using EoE.Governance;
using EoE.Network.Packets;

namespace EoE.Trade
{
    /// <summary>
    /// The data structure of the game transactions, distinguished by Guid.
    /// </summary>
    public class GameTransaction
    {
        public string Offeror { get; }
        public Guid Id { get; }
        public bool IsOpen { get; }
        public string? Recipient { get; }

        public List<ResourceStack> OfferorOffer { get; } = new List<ResourceStack>();
        public List<ResourceStack> RecipientOffer { get; } = new List<ResourceStack>();

        /// <summary>
        /// 
        /// </summary>
        /// <param invitorName="offeror"></param>
        /// <param invitorName="id"></param>
        /// <param invitorName="offerorOffer"> should contain 6 ResourceStack, if don't offer, set ResourceStack.Count = 0 </param>
        /// <param invitorName="recipientOffer">should contain 6 ResourceStack, if don't offer, set ResourceStack.Count = 0</param>
        /// <param invitorName="isOpen"></param>
        /// <param invitorName="recipient"></param>
        /// <exception cref="Exception"></exception>
        public GameTransaction(
            string offeror,
            Guid id,
            List<ResourceStack> offerorOffer,
            List<ResourceStack> recipientOffer,
            bool isOpen,
            string? recipient
            )
        {
            if (offerorOffer.Count != 6 || recipientOffer.Count != 6)
            {
                throw new Exception();
            }
            Offeror = offeror;
            Id = id;
            OfferorOffer = offerorOffer;
            RecipientOffer = recipientOffer;
            Recipient = recipient;
            IsOpen = isOpen;
            Recipient = recipient;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            else
            {
                GameTransaction transaction = (GameTransaction)obj;
                if (transaction.Id.Equals(Id))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public static Encoder<GameTransaction> encoder = (obj, writer) =>
        {
            writer.Write(obj.Offeror);
            writer.Write(obj.Id.ToByteArray());
            writer.Write(obj.IsOpen);
            if (!obj.IsOpen)
            {
                writer.Write(obj.Recipient!);
            }
            foreach (var item in obj.OfferorOffer)
            {
                ResourceStack.encoder(item, writer);
            }
            foreach (var item in obj.RecipientOffer)
            {
                ResourceStack.encoder(item, writer);
            }
        };

        public static Decoder<GameTransaction> decoder = (reader) =>
        {
            string offer = reader.ReadString();
            byte[] bytes = reader.ReadBytes(16);
            Guid id = new Guid(bytes);
            bool isOpen = reader.ReadBoolean();
            string? recipient = null;
            if (!isOpen)
            {
                recipient = reader.ReadString();
            }
            List<ResourceStack> offerorOffer = new List<ResourceStack>();
            List<ResourceStack> recipientOffer = new List<ResourceStack>();


            for (int i = 0; i < 6; i++)
            {
                offerorOffer.Add(ResourceStack.decoder(reader));
            }
            for (int i = 0; i < 6; i++)
            {
                recipientOffer.Add(ResourceStack.decoder(reader));
            }

            GameTransaction transaction;
            transaction = new GameTransaction(offer, id, offerorOffer, recipientOffer, isOpen, recipient);
            return transaction;
        };

    }
}
