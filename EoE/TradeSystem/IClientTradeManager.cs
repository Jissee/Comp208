using EoE.GovernanceSystem;

namespace EoE.TradeSystem
{
    public interface IClientTradeManager : ITradeManager
    {
        List<GameTransaction> OpenOrders { get; }
        void RequireCreateOponTransaction(GameTransaction transaction);
        GameTransaction GetGameTransaction(int transactionNumber);

        void ShowAndSelectTransaction(GameResourceType type);
        void RequireCreateSecretTransaction(GameTransaction transaction);
        void RequireCancelOpenTransaction(GameTransaction transaction);
        void RequireAcceptOpenTransaction(GameTransaction transaction);
        void RequireAlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer);
        void RequireAcceptSecretTransaction(GameTransaction transaction);
        void RequireRejectSecretTransaction(GameTransaction transaction);
        void AdddOpenTransaction(GameTransaction transaction);
        void RemoveOpenTransaction(GameTransaction transaction);
        void Synchronize(List<GameTransaction> list);
        void ShowTranscations(List<GameTransaction> list);
    }
}
