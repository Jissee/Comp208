using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.TradeSystem
{
    public class Transaction
    {
        public string Offeror { get; init; }
        public Guid Id { get; init; }

        public bool IsOpen { get; init; }
        public ResourceStack OfferorOffer { get; private set; }
        public ResourceStack RecipientOffer { get; private set; }

        public Transaction(string offeror,Guid id, ResourceStack offerorOffer, ResourceStack recipientOffer,bool isOpen) 
        {
            this.Offeror = offeror;
            this.Id = id;
            OfferorOffer = offerorOffer;
            RecipientOffer = recipientOffer;
            this.IsOpen = isOpen;
        }
    }
}
