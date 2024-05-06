namespace EoE.Treaty
{
    public interface ITreaty
    {
        public IPlayer FirstParty { get; init; }
        public IPlayer SecondParty { get; init; }
    }
}
