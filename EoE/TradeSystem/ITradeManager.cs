namespace EoE.TradeSystem
{
    public interface ITradeManager
    {
        List<GameTransaction> OpenOrders { get; }
        void CreateOpenTransaction(GameTransaction transaction);
        void CreateSecretTransaction(GameTransaction transaction);
    }
}
