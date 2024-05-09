namespace EoE.Treaty
{
    /// <summary>
    /// Represents a treaty. It has two players.
    /// </summary>
    public interface ITreaty
    {
        public IPlayer FirstParty { get; init; }
        public IPlayer SecondParty { get; init; }
    }
}
