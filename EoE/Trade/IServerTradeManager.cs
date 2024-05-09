using EoE.Governance;

namespace EoE.Trade
{
    /// <summary>
    /// Server side interface of the trade manager, including the game logic and boundary check.
    /// </summary>
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
