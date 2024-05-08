namespace EoE.Governance
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

            CountryPrimaryModifier.AddNode(CountryResourceModifier);
            CountrySecondaryModifier.AddNode(CountryResourceModifier);

            CountrySiliconModifier.AddNode(CountryPrimaryModifier);
            CountryCopperModifier.AddNode(CountryPrimaryModifier);
            CountryIronModifier.AddNode(CountryPrimaryModifier);
            CountryAluminumModifier.AddNode(CountryPrimaryModifier);

            CountryElectronicModifier.AddNode(CountrySecondaryModifier);
            CountryIndustralModifier.AddNode(CountrySecondaryModifier);


            CountrySiliconModifier.AddNode(globalGameStatus.GlobalSiliconModifier);
            CountryCopperModifier.AddNode(globalGameStatus.GlobalCopperModifier);
            CountryIronModifier.AddNode(globalGameStatus.GlobalIronModifier);
            CountryAluminumModifier.AddNode(globalGameStatus.GlobalAluminumModifier);
            CountryElectronicModifier.AddNode(globalGameStatus.GlobalElectronicModifier);
            CountryIndustralModifier.AddNode(globalGameStatus.GlobalIndustralModifier);
        }
    }
}
