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
        private int[] populationAllocation = new int [6];
        public int AvailablePopulationt { get; set; }
        public Productivity PlayerProductivity { get; }
        public PlayerGonverance()
        {
            FieldList = new PlayerFieldList();
            ResourceList = new PlayerResourceList();
            populationAllocation = new int[6];
            for (int i = 0; i < populationAllocation.Length; i++)
            {
                populationAllocation[i] = 0;
            }
            PlayerProductivity = new Productivity();
            AvailablePopulationt = 100;
        }

        public int GetTotalPopulation()
        {
            int count = 0;
            for (int i = 0; i < populationAllocation.Length; i++)
            {
                count += populationAllocation[i];
            }
            count += AvailablePopulationt;
            return count;
        }

        public int[] GetPopulationAllocation()
        {
            return populationAllocation;
        }



        private void UpdatePrimaryResource()
        {
            float produceRate = PlayerProductivity.PrimaryProductivity * PlayerProductivity.SiliconProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountrySilicon.Add( Recipies.producePrimaryResource(populationAllocation[0], produceRate,FieldList.CountryFieldSilicon,null,null,null,null));

            produceRate = PlayerProductivity.PrimaryProductivity * PlayerProductivity.CopperProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountryCopper.Add( Recipies.producePrimaryResource(populationAllocation[1], produceRate, FieldList.CountryFieldCopper, null, null,null,null));

            produceRate = PlayerProductivity.PrimaryProductivity * PlayerProductivity.IronProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountryIron.Add( Recipies.producePrimaryResource(populationAllocation[2], produceRate, FieldList.CountryFieldIron, null, null,null,null));

            produceRate = PlayerProductivity.PrimaryProductivity * PlayerProductivity.AluminumProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountryAluminum.Add( Recipies.producePrimaryResource(populationAllocation[3], produceRate, FieldList.CountryFieldAluminum, null, null,null,null));
        }

        private void UpdateSecondaryResource()
        {
            float produceRate = PlayerProductivity.SecondaryProductivity * PlayerProductivity.IndustrialProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountryIndustry.Add(Recipies.produceSecondaryResource(populationAllocation[4], produceRate, FieldList.CountryFieldIndustry,
                ResourceList.CountryIron, ResourceList.CountryAluminum, PlayerProductivity.IronSynthetic, PlayerProductivity.AluminumSynthetic));

            produceRate = PlayerProductivity.SecondaryProductivity * PlayerProductivity.ElectronicProdcutivity * PlayerProductivity.ProductionBonus;
            ResourceList.CountryIndustry.Add(Recipies.produceSecondaryResource(populationAllocation[5], produceRate, FieldList.CountryFieldElectronic,
                ResourceList.CountrySilicon, ResourceList.CountryCopper, PlayerProductivity.SiliconSynthetic, PlayerProductivity.CopperSynthetic));
        }

        public void Tick()
        {
            UpdatePrimaryResource();
            UpdateSecondaryResource();
        }
    }
}
