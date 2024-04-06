using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public delegate ResourceStack Recipe(
        int popCount,
        FieldStack? producingFields,
        ResourceStack? input1,
        ResourceStack? input2
    );

    public delegate int PopModel(
        int popCount,
        int totalLack
    );

    public delegate (int, ResourceStack) ArmyPrduce(
       ArmyStack requiredArmy
   );
    public static class Recipes
    {
        private static float PrimaryProdcutivity = 5.0f;
        private static float SecondaryProdcutivity =0.5f;

        public static int SiliconSynthetic = 2;
        public static int CopperSynthetic = 2;
        public static int IronSynthetic = 2;
        public static int AluminumSynthetic = 2;

        public static readonly int POP_GROWTH_THRESHOLD = 10000;

        private static int maxAllocation = 50;

        public static int InformativePopSynthetic = 2;
        public static int InformativeResourceSynthetic = 2;
        public static int MechanismPopSynthetic = 2;
        public static int MechanismResourceSynthetic = 2;
        public static int BattlePopSynthetic = 2;

        public static Recipe producePrimaryResource = (population,fields,_,_) =>
        {
            int count = (int)(Math.Min(population, maxAllocation * fields.Count) * PrimaryProdcutivity);
            return new ResourceStack(fields.Type, count);
        };
        public static Recipe produceElectronic = (population, fields, Silicon, Copper) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * SecondaryProdcutivity);
            if (Silicon.Count >= expectProduce * SiliconSynthetic && Copper.Count >= expectProduce * CopperSynthetic)
            {
                Silicon.Count -= expectProduce * SiliconSynthetic;
                Copper.Count -= expectProduce * CopperSynthetic;
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (Silicon.Count >= Copper.Count)
                {
                    acutalProduce = Silicon.Count / SiliconSynthetic;
                }
                else
                {
                    acutalProduce = Copper.Count / CopperSynthetic;
                }
                Silicon.Count -= acutalProduce * SiliconSynthetic;
                Copper.Count -= acutalProduce * CopperSynthetic;

                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static Recipe produceIndustry = (population, fields, Iron, Aluminum) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * SecondaryProdcutivity);

            if (Iron.Count >= expectProduce * IronSynthetic && Aluminum.Count >= expectProduce * AluminumSynthetic)
            {
                Iron.Count -= expectProduce * IronSynthetic;
                Aluminum.Count -= expectProduce * AluminumSynthetic;
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (Iron.Count >= Aluminum.Count)
                {
                    acutalProduce = Iron.Count / IronSynthetic;
                }
                else
                {
                    acutalProduce = Aluminum.Count / AluminumSynthetic;
                }
                Iron.Count -= acutalProduce * IronSynthetic;
                Aluminum.Count -= acutalProduce * AluminumSynthetic;

                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static PopModel calcPopGrowthProgress = (population, surplus ) =>
        {
            if (surplus >= POP_GROWTH_THRESHOLD)
            {
                population *= 2;
                // to do exposion growth
            }
            else if (surplus >= 0)
            {
                population = (int)(population * 1.1f);
                // to do smooth growth
            }
            else if (surplus <= -POP_GROWTH_THRESHOLD)
            {
                population = -(int)(population * 2.0f);
                // to do exposion decrease
            }
            else if (surplus < 0)
            {
                population = -(int)(population * 1.1f);
                // to do smooth decrease
            }
            

            return population;
        };

        public static ArmyPrduce BattleArmyproduce = (requiredArmy) =>
        {
            return (requiredArmy.Count * BattlePopSynthetic, ResourceStack.EMPTY);
        };

        public static ArmyPrduce produceInfomativeArmy = (requiredArmy) =>
        {
            return (requiredArmy.Count * InformativePopSynthetic,
            new ResourceStack(GameResourceType.Electronic, requiredArmy.Count * InformativeResourceSynthetic));
        };

        public static ArmyPrduce produceMechanismArmy = (requiredArmy) =>
        {
            return (requiredArmy.Count * MechanismPopSynthetic,
            new ResourceStack(GameResourceType.Industrial, requiredArmy.Count * MechanismResourceSynthetic));
        };

    }
}
