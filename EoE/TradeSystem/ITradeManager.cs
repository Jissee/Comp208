using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.TradeSystem
{
    public interface ITradeManager
    {
        public void CreatOponTransaction(Transaction transaction);
        public void CreatSecretTransaction(Transaction transaction);
        public void CancelOpenTransaction(Guid id, string operatorName);
        public void AcceptOpenTransaction(Guid id, string recipientName);
        public void AlterOpenTransaction(Guid id, ResourceStack offerorOffer, ResourceStack recipientOffer);
        public void AcceptSecretTransaction(Transaction transaction);
    }
}
