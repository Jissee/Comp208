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
        private static double PrimaryProdcutivity = 5.0f;
        private static double SecondaryProdcutivity =0.5f;

        public static double SiliconSynthetic = 1;
        public static double CopperSynthetic = 1.1;
        public static double IronSynthetic = 1.2;
        public static double AluminumSynthetic = 1.1;

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
                    acutalProduce = (int)(silicon/ SiliconSynthetic);
                }
                else
                {
                    acutalProduce = (int)(copper/ CopperSynthetic);
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
                    acutalProduce = (int)(iron/ IronSynthetic);
                }
                else
                {
                    acutalProduce = (int)(aluminum/ AluminumSynthetic);
                }
                return new ResourceStack(fields.Type, acutalProduce);
            }
        };

        public static ProductionConsume calcElectronicPC = (electronic) =>
        {
            ResourceStack silicon = new ResourceStack(GameResourceType.Silicon, (int)(SiliconSynthetic * electronic.Count));
            ResourceStack coppor = new ResourceStack(GameResourceType.Copper, (int)(CopperSynthetic * electronic.Count));
            return (silicon, coppor);
        };

        public static ProductionConsume calcIndustrailPC = (industrail) =>
        {
            ResourceStack iron = new ResourceStack(GameResourceType.Iron, (int)(IronSynthetic * industrail.Count));
            ResourceStack aluminum = new ResourceStack(GameResourceType.Aluminum, (int)(AluminumSynthetic * industrail.Count));
            return (iron, aluminum);
        };

        public static int calcPopGrowthProgress(int popCount, int silicon, int copper, int iron, int aluminum)
        {
            double rate = 0.3f;
            double K;

            
            Dictionary<GameResourceType, double> resourceSynthetic = new Dictionary<GameResourceType, double>()
            {
                { GameResourceType.Silicon, ServerPlayerGonverance.SILICON_PER_POP_TICK },
                { GameResourceType.Copper, ServerPlayerGonverance.COPPER_PER_POP_TICK },
                { GameResourceType.Iron, ServerPlayerGonverance.IRON_PER_POP_TICK },
                { GameResourceType.Aluminum, ServerPlayerGonverance.ALUMINUM_PER_POP_TICK }
            };


            double minK = double.MaxValue;
            minK = Math.Min(minK, silicon / resourceSynthetic[GameResourceType.Silicon]);
            minK = Math.Min(minK, copper / resourceSynthetic[GameResourceType.Copper]);
            minK = Math.Min(minK, iron / resourceSynthetic[GameResourceType.Iron]);
            minK = Math.Min(minK, aluminum / resourceSynthetic[GameResourceType.Aluminum]);

            K = minK;
            double popGrowth = popCount * rate * (1 - (double)popCount / K);

            return (int)popGrowth;
        }

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
