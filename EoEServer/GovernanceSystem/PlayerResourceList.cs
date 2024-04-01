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
        private ResourceStack countrySilicon;
        private ResourceStack countryCopper;
        private ResourceStack countryIron;
        private ResourceStack countryAluminum;
        private ResourceStack countryElectronic;
        private ResourceStack countryIndustry;

        private int siliconProduceRate;

        public PlayerResourceList()
        {
            countrySilicon = new ResourceStack(GameResourceType.Silicon, 0);
            countryCopper = new ResourceStack(GameResourceType.Copper, 0);
            countryIron = new ResourceStack(GameResourceType.Iron, 0);
            countryAluminum = new ResourceStack(GameResourceType.Aluminum, 0);
            countryElectronic = new ResourceStack(GameResourceType.Electronic, 0);
            countryIndustry = new ResourceStack(GameResourceType.Industrial, 0);
        }

        public void AddResource(ResourceStack adder)
        {
            GameResourceType type = adder.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    countrySilicon.Add(adder);
                    break;
                case GameResourceType.Copper:
                    countryCopper.Add(adder);
                    break;
                case GameResourceType.Iron:
                    countryIron.Add(adder);
                    break;
                case GameResourceType.Aluminum:
                    countryAluminum.Add(adder);
                    break;
                case GameResourceType.Electronic:
                    countryElectronic.Add(adder);
                    break;
                case GameResourceType.Industrial:
                    countryIndustry.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }

        public ResourceStack SplitResource(ResourceStack target, int count)
        {
            if (target.Count >= count)
            {
                return target.Split(count);
            }
            else
            {
                return target.Split(target.Count);
            }
        }

        public int GetResourceCount(GameResourceType type)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    return countrySilicon.Count;
                case GameResourceType.Copper:
                    return countryCopper.Count;
                case GameResourceType.Iron:
                    return countryIron.Count;
                case GameResourceType.Aluminum:
                    return countryAluminum.Count;
                case GameResourceType.Electronic:
                    return countryElectronic.Count;
                case GameResourceType.Industrial:
                    return countryIndustry.Count;
                default:
                    throw new Exception("no such type");
            }
        }
    }
}
