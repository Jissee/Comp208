using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerResourceList
    {
        public ResourceStack CountrySilicon { get; init; }
        public ResourceStack CountryCopper {get;init;}
        public ResourceStack CountryIron {get;init;}
        public ResourceStack CountryAluminum {get;init;}
        public ResourceStack CountryElectronic {get;init;}
        public ResourceStack CountryIndustrial {get;init;}

        public ResourceStack CountryBattleArmy { get; init; }
        public ResourceStack CountryInformativeArmy { get; init; }
        public ResourceStack CountryMechanismArmy { get; init; }
        public PlayerResourceList()
        {
            CountrySilicon = new ResourceStack(GameResourceType.Silicon, 0);
            CountryCopper = new ResourceStack(GameResourceType.Copper, 0);
            CountryIron = new ResourceStack(GameResourceType.Iron, 0);
            CountryAluminum = new ResourceStack(GameResourceType.Aluminum, 0);
            CountryElectronic = new ResourceStack(GameResourceType.Electronic, 0);
            CountryIndustrial = new ResourceStack(GameResourceType.Industrial, 0);
            CountryBattleArmy = new ResourceStack(GameResourceType.BattleArmy, 0);
            CountryInformativeArmy = new ResourceStack(GameResourceType.InformativeArmy, 0);
            CountryMechanismArmy = new ResourceStack(GameResourceType.MechanismArmy, 0);
        }

        public void AddResource(ResourceStack adder)
        {
            GameResourceType type = adder.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    CountrySilicon.Add(adder);
                    break;
                case GameResourceType.Copper:
                    CountryCopper.Add(adder);
                    break;
                case GameResourceType.Iron:
                    CountryIron.Add(adder);
                    break;
                case GameResourceType.Aluminum:
                    CountryAluminum.Add(adder);
                    break;
                case GameResourceType.Electronic:
                    CountryElectronic.Add(adder);
                    break;
                case GameResourceType.Industrial:
                    CountryIndustrial.Add(adder);
                    break;
                case GameResourceType.BattleArmy:
                    CountryBattleArmy.Add(adder);
                    break;
                case GameResourceType.InformativeArmy:
                    CountryInformativeArmy.Add(adder);
                    break;
                case GameResourceType.MechanismArmy:
                    CountryMechanismArmy.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }
        public ResourceStack SplitResourceStack(GameResourceType type, int count)
        {
            return SplitResourceStack(new ResourceStack(type, count));
        }
        public ResourceStack SplitResourceStack(ResourceStack stack)
        {
            GameResourceType type = stack.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountrySilicon.Split(stack.Count);
                case GameResourceType.Copper:
                    return CountryCopper.Split(stack.Count);
                case GameResourceType.Iron:
                    return CountryIron.Split(stack.Count);
                case GameResourceType.Aluminum:
                    return CountryAluminum.Split(stack.Count);
                case GameResourceType.Electronic:
                    return CountryElectronic.Split(stack.Count);
                case GameResourceType.Industrial:
                    return CountryIndustrial.Split(stack.Count);
                case GameResourceType.BattleArmy:
                    return CountryBattleArmy.Split(stack.Count);
                case GameResourceType.InformativeArmy:
                    return CountryInformativeArmy.Split(stack.Count);
                case GameResourceType.MechanismArmy:
                    return CountryMechanismArmy.Split(stack.Count);
                default:
                    throw new Exception("no such type");
            }
        }
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
