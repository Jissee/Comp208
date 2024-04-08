namespace EoE.GovernanceSystem
{
    public interface IPlayerResourceList
    {
        public ResourceStack CountrySilicon { get; init; }
        public ResourceStack CountryCopper { get; init; }
        public ResourceStack CountryIron { get; init; }
        public ResourceStack CountryAluminum { get; init; }
        public ResourceStack CountryElectronic { get; init; }
        public ResourceStack CountryIndustrial { get; init; }

        public ResourceStack CountryBattleArmy { get; init; }
        public ResourceStack CountryInformativeArmy { get; init; }
        public ResourceStack CountryMechanismArmy { get; init; }

        public int GetResourceCount(GameResourceType type)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountrySilicon.Count;
                case GameResourceType.Copper:
                    return CountryCopper.Count;
                case GameResourceType.Iron:
                    return CountryIron.Count;
                case GameResourceType.Aluminum:
                    return CountryAluminum.Count;
                case GameResourceType.Electronic:
                    return CountryElectronic.Count;
                case GameResourceType.Industrial:
                    return CountryIndustrial.Count;
                case GameResourceType.BattleArmy:
                    return CountryBattleArmy.Count;
                case GameResourceType.InformativeArmy:
                    return CountryInformativeArmy.Count;
                case GameResourceType.MechanismArmy:
                    return CountryMechanismArmy.Count;
                default:
                    throw new Exception("no such type");
            }
        }
    }
}
