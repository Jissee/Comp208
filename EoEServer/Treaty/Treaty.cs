using EoE.Treaty;

namespace EoE.Server.Treaty
{
    public class Treaty : ITreaty
    {
        public IPlayer FirstParty { get; init; }
        public IPlayer SecondParty { get; init; }

        public Treaty(IPlayer firstParty, IPlayer secondParty)
        {
            FirstParty = firstParty;
            SecondParty = secondParty;
        }
    }
}
