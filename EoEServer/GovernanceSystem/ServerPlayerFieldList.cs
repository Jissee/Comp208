using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerFieldList:IFieldList
    {
        public FieldStack CountryFieldSilicon { get; init;}
        public FieldStack CountryFieldCopper{get; init;}
        public FieldStack CountryFieldIron{get; init;}
        public FieldStack CountryFieldAluminum{get; init;}
        public FieldStack CountryFieldElectronic{get; init;}
        public FieldStack CountryFieldIndustry{get; init;}
        public int TotalFieldCount => CountryFieldSilicon.Count + CountryFieldCopper.Count +
            CountryFieldAluminum.Count + CountryFieldIron.Count +
            CountryFieldElectronic.Count + CountryFieldIndustry.Count;

        public ServerPlayerFieldList(
            int silicon,
            int copper,
            int iron,
            int aluminum,
            int electronic,
            int industry
            )
        {
            CountryFieldSilicon = new FieldStack(GameResourceType.Silicon, silicon);
            CountryFieldCopper = new FieldStack(GameResourceType.Copper, copper);
            CountryFieldIron = new FieldStack(GameResourceType.Iron, iron);
            CountryFieldAluminum = new FieldStack(GameResourceType.Aluminum, aluminum);
            CountryFieldElectronic = new FieldStack(GameResourceType.Electronic, electronic);
            CountryFieldIndustry = new FieldStack(GameResourceType.Industrial, industry);
        }

        public ServerPlayerFieldList() : this(20, 20, 20, 20, 20, 20)
        {

        }

        public void addField(FieldStack adder)
        {
            GameResourceType type = adder.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    CountryFieldSilicon.Add(adder);
                    break;
                case GameResourceType.Copper:
                    CountryFieldCopper.Add(adder);
                    break;
                case GameResourceType.Iron:
                    CountryFieldIron.Add(adder);
                    break;
                case GameResourceType.Aluminum:
                    CountryFieldAluminum.Add(adder);
                    break;
                case GameResourceType.Electronic:
                    CountryFieldElectronic.Add(adder);
                    break;
                case GameResourceType.Industrial:
                    CountryFieldIndustry.Add(adder);
                    break;
                default:
                    throw new Exception("no such type");
            }
        }

        public FieldStack SplitFidle(GameResourceType type, int count)
        {
           return SplitFidle(new FieldStack(type, count));
        }
        public FieldStack SplitFidle(FieldStack field)
        {
            GameResourceType type = field.Type;
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountryFieldSilicon.Split(field.Count);
                case GameResourceType.Copper:
                    return CountryFieldCopper.Split(field.Count);
                case GameResourceType.Iron:
                    return CountryFieldIron.Split(field.Count);
                case GameResourceType.Aluminum:
                    return CountryFieldAluminum.Split(field.Count);
                case GameResourceType.Electronic:
                    return CountryFieldElectronic.Split(field.Count);
                case GameResourceType.Industrial:
                    return CountryFieldIndustry.Split(field.Count);
                default:
                    throw new Exception("no such type");
            }
        }

        public int GetFieldCount(GameResourceType type)
        {
            return ((IFieldList)this).GetFieldCount(type);
        }
    }
  
}