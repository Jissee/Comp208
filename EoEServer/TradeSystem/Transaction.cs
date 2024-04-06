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
        private ServerPlayer offeror; 
        private ServerPlayer? recipient;
        private bool offerorConfirmed;
        private bool recipientConfirmed;
        public Dictionary<GameResourceType, int> offerorOffer = new Dictionary<GameResourceType, int>();
        public Dictionary<GameResourceType, int> recipientOffer = new Dictionary<GameResourceType, int>();

        public Transaction(ServerPlayer offeror) 
        {
            this.offeror = offeror;
            offerorConfirmed = false;
            recipientConfirmed = false;
        }
        public void SetRecipient(ServerPlayer recipient)
        {
            this.recipient = recipient;
        }
        public void ClearRecipient()
        {
            recipient = null;
        }
        public void AddOfferorOffer(GameResourceType type, int count)
        {
            AddOfferorOffer(new ResourceStack(type, count));
        }
        public void AddOfferorOffer(ResourceStack stack)
        {
            if(offerorOffer.ContainsKey(stack.Type))
            {
                offerorOffer[stack.Type] += stack.Count;
            }
            else
            {
                offerorOffer.Add(stack.Type, stack.Count);
            }
        }
        public bool RemoveOfferorOffer(GameResourceType type, int count)
        {
            if(offerorOffer.ContainsKey(type))
            {
                int offeredCount = offerorOffer[type];
                if (offeredCount < count)
                {
                    return false;
                }
                else
                {
                    offerorOffer[type] -= count;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public void AddRecipientOffer(GameResourceType type, int count)
        {
            AddRecipientOffer(new ResourceStack(type, count));
        }
        public void AddRecipientOffer(ResourceStack stack)
        {
            if (recipientOffer.ContainsKey(stack.Type))
            {
                recipientOffer[stack.Type] += stack.Count;
            }
            else
            {
                recipientOffer.Add(stack.Type, stack.Count);
            }
        }
        public bool RemoveRecipientOffer(GameResourceType type, int count)
        {
            if (recipientOffer.ContainsKey(type))
            {
                int offeredCount = recipientOffer[type];
                if (offeredCount < count)
                {
                    return false;
                }
                else
                {
                    recipientOffer[type] -= count;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void OfferorDeal()
        {
            this.offerorConfirmed = true;
        }
        public void RecipientDeal()
        {
            this.recipientConfirmed = true;

        }

    }
}
