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
    public class ClientResourceList : IClientResourceList
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

        public ClientResourceList(
          int silicon,
          int copper,
          int iron,
          int aluminum,
          int electronic,
          int industrial,
          int battleArmy,
          int informativeArmy,
          int mechanismArmy)
        {
            CountrySilicon = new ResourceStack(GameResourceType.Silicon, silicon);
            CountryCopper = new ResourceStack(GameResourceType.Copper, copper);
            CountryIron = new ResourceStack(GameResourceType.Iron, iron);
            CountryAluminum = new ResourceStack(GameResourceType.Aluminum, aluminum);
            CountryElectronic = new ResourceStack(GameResourceType.Electronic, electronic);
            CountryIndustrial = new ResourceStack(GameResourceType.Industrial, industrial);
            CountryBattleArmy = new ResourceStack(GameResourceType.BattleArmy, battleArmy);
            CountryInformativeArmy = new ResourceStack(GameResourceType.InformativeArmy, informativeArmy);
            CountryMechanismArmy = new ResourceStack(GameResourceType.MechanismArmy, mechanismArmy);
        }
        public ClientResourceList() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public void Synchronization(ResourceListRecord resourceListRecord)
        {
            CountrySilicon.Count = resourceListRecord.siliconCount;
            CountryCopper.Count = resourceListRecord.copperCount;
            CountryIron.Count = resourceListRecord.ironCount;
            CountryAluminum.Count = resourceListRecord.aluminumCount;
            CountryElectronic.Count = resourceListRecord.electronicCount;
            CountryIndustrial.Count = resourceListRecord.industrialCount;

            CountryBattleArmy.Count = resourceListRecord.battleArmyCount;
            CountryInformativeArmy.Count = resourceListRecord.informativeArmyCount;
            CountryMechanismArmy.Count = resourceListRecord.mechanismArmyCount;
        }
    }
}
