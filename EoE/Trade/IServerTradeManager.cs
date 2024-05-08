using EoE.Governance;

namespace EoE.Trade
{
    public interface IServerTradeManager : ITradeManager
    {
        void CancelOpenTransaction(Guid id, string operatorName);
        void AcceptOpenTransaction(Guid id, string recipientName);
        void AlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer, string operatorName);
        void AcceptSecretTransaction(GameTransaction transaction);
        void RejectSecretTransaction(GameTransaction transaction, string operatorName);
        void ClearAll(IPlayer offeror);
    }
}
