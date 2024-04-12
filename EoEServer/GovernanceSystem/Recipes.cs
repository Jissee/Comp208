using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
using EoE.WarSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public delegate ResourceStack Produce(
        int popCount,
        FieldStack producingFields,
        int input1,
        int input2
    );

    public delegate (ResourceStack, ResourceStack) ProductionConsume(
        ResourceStack input
    );

    public delegate int PopModel(
        int popCount,
        int silicon,
        int copper,
        int iron,
        int aluminum
    );

    public delegate (int, ResourceStack) ArmyPrduce(
       ResourceStack requiredArmy
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

        public static Produce calcPrimaryP = (population,fields,_,_) =>
        {
            int count = (int)(Math.Min(population, maxAllocation * fields.Count) * PrimaryProdcutivity);
            return new ResourceStack(fields.Type, count);
        };
        public static Produce calcElectronicP = (population, fields, silicon, copper) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * SecondaryProdcutivity);
            if (silicon >= expectProduce * SiliconSynthetic && copper>= expectProduce * CopperSynthetic)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (silicon>= copper)
                {
                    acutalProduce = silicon/ SiliconSynthetic;
                }
                else
                {
                    acutalProduce = copper/ CopperSynthetic;
                }
                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static Produce calcIndustrailP = (population, fields, iron, aluminum) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * SecondaryProdcutivity);

            if (iron>= expectProduce * IronSynthetic && aluminum>= expectProduce * AluminumSynthetic)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (iron>= aluminum)
                {
                    acutalProduce = iron/ IronSynthetic;
                }
                else
                {
                    acutalProduce = aluminum/ AluminumSynthetic;
                }
                return new ResourceStack(fields.Type, acutalProduce);
            }
        };

        public static ProductionConsume calcElectronicPC = (electronic) =>
        {
            ResourceStack silicon = new ResourceStack(GameResourceType.Silicon, SiliconSynthetic * electronic.Count);
            ResourceStack coppor = new ResourceStack(GameResourceType.Copper, CopperSynthetic * electronic.Count);
            return (silicon, coppor);
        };

        public static ProductionConsume calcIndustrailPC = (industrail) =>
        {
            ResourceStack iron = new ResourceStack(GameResourceType.Iron, IronSynthetic * industrail.Count);
            ResourceStack aluminum = new ResourceStack(GameResourceType.Aluminum, AluminumSynthetic * industrail.Count);
            return (iron, aluminum);
        };

        public static PopModel calcPopGrowthProgress = (popCount, silicon, copper, iron, aluminum) =>
        {
            //TODO

            return 0;
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
