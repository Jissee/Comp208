using EoE.Governance;

namespace EoE.Trade
{
    /// <summary>
    /// Client side interface of the trade manager, including request and query data from the server.
    /// </summary>
    public interface IClientTradeManager : ITradeManager
    {
        void CancelOpenTransaction(GameTransaction transaction);
        void AcceptOpenTransaction(GameTransaction transaction);
        void AlterOpenTransaction(Guid id, List<ResourceStack> offerorOffer, List<ResourceStack> recipientOffer);
        void AcceptSecretTransaction(GameTransaction transaction);
        void RejectSecretTransaction(GameTransaction transaction);
        GameTransaction GetGameTransaction(int transactionNumber);
        void ShowAndSelectTransaction(GameResourceType type);
        void AddOpenTransaction(GameTransaction transaction);
        void RemoveOpenTransaction(GameTransaction transaction);
        void Synchronize(List<GameTransaction> list);
        void ShowTranscations(List<GameTransaction> list);
    }
}
