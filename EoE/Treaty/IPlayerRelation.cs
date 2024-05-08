namespace EoE.Treaty
{
    public interface IPlayerRelation
    {
        Dictionary<IPlayer, List<IPlayer>> ProtectedBy { get; }
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
    }
}
