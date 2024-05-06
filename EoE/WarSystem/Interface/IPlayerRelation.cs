namespace EoE.WarSystem.Interface
{
    public interface IPlayerRelation
    {
        Dictionary<IPlayer, List<IPlayer>> ProtectedBy { get; set; }
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
    }
}
