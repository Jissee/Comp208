using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IFieldList
    {
        public FieldStack CountryFieldSilicon { get;}
        public FieldStack CountryFieldCopper { get;}
        public FieldStack CountryFieldIron { get;}
        public FieldStack CountryFieldAluminum { get;}
        public FieldStack CountryFieldElectronic { get;}
        public FieldStack CountryFieldIndustry { get;}
        public int TotalFieldCount { get; }

        public int GetFieldCount(GameResourceType type)
        {
            switch (type)
            {
                case GameResourceType.Silicon:
                    return CountryFieldSilicon.Count;
                case GameResourceType.Copper:
                    return CountryFieldCopper.Count;
                case GameResourceType.Iron:
                    return CountryFieldIron.Count;
                case GameResourceType.Aluminum:
                    return CountryFieldAluminum.Count;
                case GameResourceType.Electronic:
                    return CountryFieldElectronic.Count;
                case GameResourceType.Industrial:
                    return CountryFieldIndustry.Count;
                default:
                    throw new Exception("no such type");
            }
        }
    }


}
