using System;
using EoE.GovernanceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerGonverance : ITickable
    {
        public PlayerFieldList FieldList { get; }
        public PlayerResourceList ResourceList { get; }

        public Modifier CountryResourceModifier { get; init; }
        public Modifier CountryPrimaryModifier { get; init; }
        public Modifier CountrySecondaryModifier { get; init; }
        public Modifier CountrySiliconModifier { get; init; }
        public Modifier CountryCopperModifier { get; init; }
        public Modifier CountryIronModifier { get; init; }
        public Modifier CountryAluminumModifier { get; init; }


        public int TotalPopulation => FieldList.TotalPopulation;
        public Productivity PlayerProductivity { get; }
        public PlayerGonverance()
        {
            FieldList = new PlayerFieldList();
            ResourceList = new PlayerResourceList();
            PlayerProductivity = new Productivity();

            CountryResourceModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryPrimaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySecondaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySiliconModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryCopperModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryIronModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryAluminumModifier = new Modifier("", Modifier.ModifierType.Plus);
        }

        public void PrepareModifier(Server server)
        {
            CountrySiliconModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalSiliconModifier)
                .AddNode(server.GlobalPrimaryModifier)
                .AddNode(server.GlobalResourceModifier);




        }

        private void UpdatePrimaryResource()
        {
            ResourceStack producedSilicon = Recipies.producePrimaryResource(FieldList.SiliconPop, FieldList.CountryFieldSilicon, null, null);
            producedSilicon.Count = (int)CountrySiliconModifier.Apply(producedSilicon.Count);
            ResourceList.CountrySilicon.Add(producedSilicon);


            ResourceList.CountryCopper.Add(Recipies.producePrimaryResource(FieldList.CopperPop, FieldList.CountryFieldCopper, null, null));
            ResourceList.CountryIron.Add(Recipies.producePrimaryResource(FieldList.IronPop, FieldList.CountryFieldIron, null, null));
            ResourceList.CountryAluminum.Add(Recipies.producePrimaryResource(FieldList.AluminumPop, FieldList.CountryFieldAluminum, null, null));

        }

        private void UpdateSecondaryResource()
        {
            ResourceList.CountryElectronic.Add(Recipies.produceElectronic(FieldList.ElectronicPop, FieldList.CountryFieldElectronic, ResourceList.CountrySilicon, ResourceList.CountryCopper));
            ResourceList.CountryIndustry.Add(Recipies.produceIndustry(FieldList.IndustryPop, FieldList.CountryFieldIndustry, ResourceList.CountryIron, ResourceList.CountryAluminum));
        }

        public void Tick()
        {
            UpdatePrimaryResource();
            UpdateSecondaryResource();
        }
    }
}
