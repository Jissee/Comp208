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
        public ResourceStack CountrySilicon { get; set; }
        public ResourceStack CountryCopper {get;set;}
        public ResourceStack CountryIron {get;set;}
        public ResourceStack CountryAluminum {get;set;}
        public ResourceStack CountryElectronic {get;set;}
        public ResourceStack CountryIndustry {get;set;}

        public PlayerResourceList()
        {
            CountrySilicon = new ResourceStack(GameResourceType.Silicon, 0);
            CountryCopper = new ResourceStack(GameResourceType.Copper, 0);
            CountryIron = new ResourceStack(GameResourceType.Iron, 0);
            CountryAluminum = new ResourceStack(GameResourceType.Aluminum, 0);
            CountryElectronic = new ResourceStack(GameResourceType.Electronic, 0);
            CountryIndustry = new ResourceStack(GameResourceType.Industrial, 0);
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
                    CountryIndustry.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }

        public ResourceStack SplitFidle(ResourceStack field)
        {
            GameResourceType type = field.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountrySilicon.Split(field);
                case GameResourceType.Copper:
                    return CountryCopper.Split(field);
                case GameResourceType.Iron:
                    return CountryIron.Split(field);
                case GameResourceType.Aluminum:
                    return CountryAluminum.Split(field);
                case GameResourceType.Electronic:
                    return CountryElectronic.Split(field);
                case GameResourceType.Industrial:
                    return CountryIndustry.Split(field);
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
                    return CountryIndustry.Count;
                default:
                    throw new Exception("no such type");
            }
        }
    }
}
