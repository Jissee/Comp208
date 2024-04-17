using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.TradeSystem
{
    public interface IClientTradeManager: ITradeManager
    {
        void RequireCreateOponTransaction(GameTransaction transaction);
        GameTransaction GetGameTransaction(int transactionNumber);
        void RequireCreateSecretTransaction(GameTransaction transaction);
        void RequireCancelOpenTransaction(GameTransaction transaction);
        void RequireAcceptOpenTransaction(GameTransaction transaction);
        void RequireAlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer);
        void RequireAcceptSecretTransaction(GameTransaction transaction);
        void RejectSecretTransaction(GameTransaction transaction);
        void CreateNewOpenTransaction(GameTransaction transaction);
        void RemoveOpenTransaction(GameTransaction transaction);
        void Synchronize(List<GameTransaction> list);
    }
}
