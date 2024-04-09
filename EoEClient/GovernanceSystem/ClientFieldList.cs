using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.GovernanceSystem
{
    public class ClientFieldList : IClientFieldList
    {
        public FieldStack CountryFieldSilicon { get; init; }
        public FieldStack CountryFieldCopper { get; init; }
        public FieldStack CountryFieldIron { get; init; }
        public FieldStack CountryFieldAluminum { get; init; }
        public FieldStack CountryFieldElectronic { get; init; }
        public FieldStack CountryFieldIndustry { get; init; }
        public int TotalFieldCount => CountryFieldSilicon.Count + CountryFieldCopper.Count +
            CountryFieldAluminum.Count + CountryFieldIron.Count +
            CountryFieldElectronic.Count + CountryFieldIndustry.Count;
        public ClientFieldList(
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

        public ClientFieldList() : this(0, 0, 0, 0, 0, 0)
        {

        }

        public void Synchronization(FieldListRecord fieldListRecord)
        {
            CountryFieldSilicon.Count = fieldListRecord.siliconFieldCount;
            CountryFieldCopper.Count = fieldListRecord.copperFieldCount;
            CountryFieldIron.Count = fieldListRecord.ironFieldCount;
            CountryFieldAluminum.Count = fieldListRecord.aluminumFieldCount;
            CountryFieldElectronic.Count = fieldListRecord.electronicFieldCount;
            CountryFieldIndustry.Count = fieldListRecord.industrialFieldCount;
        }

    }
}
