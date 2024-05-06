namespace EoE.GovernanceSystem
{
    public class PlayerStatus
    {
        public Modifier CountryResourceModifier { get; init; }
        public Modifier CountryPrimaryModifier { get; init; }
        public Modifier CountrySecondaryModifier { get; init; }
        public Modifier CountrySiliconModifier { get; init; }
        public Modifier CountryCopperModifier { get; init; }
        public Modifier CountryIronModifier { get; init; }
        public Modifier CountryAluminumModifier { get; init; }
        public Modifier CountryElectronicModifier { get; init; }
        public Modifier CountryIndustralModifier { get; init; }

        public PlayerStatus(GameStatus globalGameStatus)
        {
            CountryResourceModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryPrimaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySecondaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySiliconModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryCopperModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryIronModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryAluminumModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryElectronicModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryIndustralModifier = new Modifier("", Modifier.ModifierType.Plus);

            CountrySiliconModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(globalGameStatus.GlobalSiliconModifier)
                .AddNode(globalGameStatus.GlobalPrimaryModifier)
                .AddNode(globalGameStatus.GlobalResourceModifier);

            CountryCopperModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(globalGameStatus.GlobalCopperModifier)
                .AddNode(globalGameStatus.GlobalPrimaryModifier)
                .AddNode(globalGameStatus.GlobalResourceModifier);

            CountryIronModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(globalGameStatus.GlobalIronModifier)
                .AddNode(globalGameStatus.GlobalPrimaryModifier)
                .AddNode(globalGameStatus.GlobalResourceModifier);

            CountryAluminumModifier
               .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(globalGameStatus.GlobalAluminumModifier)
                .AddNode(globalGameStatus.GlobalPrimaryModifier)
                .AddNode(globalGameStatus.GlobalResourceModifier);

            CountryElectronicModifier
               .AddNode(CountrySecondaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(globalGameStatus.GlobalElectronicModifier)
                .AddNode(globalGameStatus.GlobalSecondaryModifier)
                .AddNode(globalGameStatus.GlobalResourceModifier);

            CountryIndustralModifier
              .AddNode(CountrySecondaryModifier)
               .AddNode(CountryResourceModifier)
               .AddNode(globalGameStatus.GlobalIndustralModifier)
               .AddNode(globalGameStatus.GlobalSecondaryModifier)
               .AddNode(globalGameStatus.GlobalResourceModifier);
        }
    }
}
