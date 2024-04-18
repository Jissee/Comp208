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

        public List<ResourceStack> OfferorOffer { get; init; } = new List<ResourceStack>();
        public List<ResourceStack> RecipientOffer { get; init; } = new List<ResourceStack>();

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
        public GameTransaction(string offeror,Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer,bool isOpen,string? recipient) 
        {
            if (offerorOffer.Count != 6 || recipientOffer.Count != 6)
            {
                throw new Exception();
            }
            this.Offeror = offeror;
            this.Id = id;
            OfferorOffer = offerorOffer;
            RecipientOffer = recipientOffer;
            Recipient = recipient;
            IsOpen = isOpen;
            Recipient = recipient;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType()!= this.GetType())
            {
                return false;
            }
            else
            {
                GameTransaction transaction = (GameTransaction)obj;
                if (transaction.Id.Equals(this.Id))
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
        public static Encoder<GameTransaction> encoder = (GameTransaction obj, BinaryWriter writer) =>
        {
            writer.Write(obj.Offeror);
            writer.Write(obj.Id.ToByteArray());
            writer.Write(obj.IsOpen);
            if (!obj.IsOpen)
            {
                writer.Write(obj.Recipient);
            }
            foreach (var item in obj.OfferorOffer)
            {
                ResourceStack.encoder(item,writer);
            }
            foreach (var item in obj.RecipientOffer)
            {
                ResourceStack.encoder(item, writer);
            }
        };

        public static Decoder<GameTransaction> decoder = (BinaryReader reader) =>
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
