namespace EoE.War.Interface
{
    /// <summary>
    /// Managing the players in the same party of the same war.
    /// </summary>
    public interface IWarParty
    {
        int WarWidth { get; }
        Dictionary<IPlayer, IArmy> Armies { get; }
        IArmy? TotalArmy { get; }
        void Clear();
        bool AllSurrendered { get; }
        void SetWar(IWar war);
        void AddPlayer(IPlayer player);
        void PlayerSurrender(IPlayer player);
        void PlayerLose(IPlayer player);
        bool Contains(IPlayer player);
        void FillInFrontier(IPlayer player, int battle, int informative, int mechanism);
        (int, int) GetMechAttackBattAttack();
        void AbsorbAttack(int MechAttack, int BattAttack);
        bool HasFilled(IPlayer player);
    }
}
