namespace EoE.Treaty
{
    /// <summary>
    /// Managing the relationship graph of the players by the treaties,
    /// </summary>
    public interface IPlayerRelation
    {
        Dictionary<IPlayer, List<IPlayer>> ProtectedBy { get; }
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
    }
}
