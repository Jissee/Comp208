using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerFieldList
    {
        private FieldStack countryFieldSilicon;
        private FieldStack countryFieldCopper;
        private FieldStack countryFieldIron;
        private FieldStack countryFieldAluminum;
        private FieldStack countryFieldElectronic;
        private FieldStack countryFieldIndustry;

        public PlayerFieldList()
        {
            countryFieldSilicon = new FieldStack(GameResourceType.Silicon, 20);
            countryFieldCopper = new FieldStack(GameResourceType.Silicon, 20);
            countryFieldIron = new FieldStack(GameResourceType.Iron, 20);
            countryFieldAluminum = new FieldStack(GameResourceType.Aluminum, 20);
            countryFieldElectronic = new FieldStack(GameResourceType.Electronic, 20);
            countryFieldIndustry = new FieldStack(GameResourceType.Industrial, 20);
        }

        public void addField(FieldStack adder)
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

        public FieldStack SplitFidle(FieldStack target, int count)
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

        public int GetFieldCount(GameResourceType type)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    return countryFieldSilicon.Count;
                case GameResourceType.Copper:
                    return countryFieldCopper.Count;
                case GameResourceType.Iron:
                    return countryFieldIron.Count;
                case GameResourceType.Aluminum:
                    return countryFieldAluminum.Count;
                case GameResourceType.Electronic:
                    return countryFieldElectronic.Count;
                case GameResourceType.Industrial:
                    return countryFieldIndustry.Count;
                default:
                    throw new Exception("no such type");
            }
        }
    }
  
}