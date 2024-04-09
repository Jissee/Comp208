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
        void CreatOponTransaction(GameTransaction transaction);
        void CreatSecretTransaction(GameTransaction transaction);
        void CancelOpenTransaction(Guid id, string operatorName);
        void AcceptOpenTransaction(Guid id, string recipientName);
        void AlterOpenTransaction(Guid id, ResourceStack offerorOffer, ResourceStack recipientOffer);
        void AcceptSecretTransaction(GameTransaction transaction);
    }
}
