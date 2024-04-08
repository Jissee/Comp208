using EoE.Network.Packets;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.GovernanceSystem.Interface
{
    public interface IResourceList
    {
        public ResourceStack CountrySilicon { get;}
        public ResourceStack CountryCopper { get;}
        public ResourceStack CountryIron { get;}
        public ResourceStack CountryAluminum { get;}
        public ResourceStack CountryElectronic { get;}
        public ResourceStack CountryIndustrial { get;}

        public ResourceStack CountryBattleArmy { get;}
        public ResourceStack CountryInformativeArmy { get;}
        public ResourceStack CountryMechanismArmy { get;}

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
