namespace EoE.Trade
{
    public interface ITradeManager
    {
        static readonly int MAX_TRANSACTION_NUMBER = 50;
        List<GameTransaction> OpenOrders { get; }
        void CreateOpenTransaction(GameTransaction transaction);
        void CreateSecretTransaction(GameTransaction transaction);
    }
}
