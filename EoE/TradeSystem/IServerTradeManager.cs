using EoE.GovernanceSystem;

namespace EoE.TradeSystem
{
    public interface IServerTradeManager : ITradeManager
    {
        public List<GameTransaction> openOrders { get; }
        void CreatOponTransaction(GameTransaction transaction);
        void CreatSecretTransaction(GameTransaction transaction);
        void CancelOpenTransaction(Guid id, string operatorName);
        void AcceptOpenTransaction(Guid id, string recipientName);
        void AlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer, string operatorName);
        void AcceptSecretTransaction(GameTransaction transaction);
        void RejectSecretTransaction(GameTransaction transaction, string operatorName);
        void ClearAll(IPlayer offeror);
    }
}
