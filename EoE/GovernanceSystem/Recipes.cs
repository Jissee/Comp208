using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
using EoE.WarSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public static readonly double SILICON_PER_POP_TICK = 1.25f;
        public static readonly double COPPER_PER_POP_TICK = 1;
        public static readonly double IRON_PER_POP_TICK = 1.5f;
        public static readonly double ALUMINUM_PER_POP_TICK = 1.125f;

        private static double ElectronicSynthetic = 4;
        private static double IndustrailSynthetic = 5;

        public static double SiliconSynthetic = 8.45;
        public static double CopperSynthetic = 6.76;
        public static double IronSynthetic = 10.14;
        public static double AluminumSynthetic = 7.61;

        public static int Silicon2Elec = 5;
        public static int Copper2Elec = 4;
        public static int Iron2Indus = 6;
        public static double Aluminum2Indus = 4.5;


        private static int maxAllocation = 160;

        public static int InformativePopSynthetic =  9;
        public static int InformativeResourceSynthetic = 3;
        public static int MechanismPopSynthetic = 12;
        public static int MechanismResourceSynthetic = 4;
        public static int BattlePopSynthetic = 5;

        public static Produce calcSiliconP = (population,fields, _, _) =>
        {
            int count = (int)(population * SiliconSynthetic);
            return new ResourceStack(fields.Type, count);
        };
        public static Produce calcCopperP = (population, fields, _, _) =>
        {
            int count = (int)(population * CopperSynthetic);
            return new ResourceStack(fields.Type, count);
        };
        public static Produce calcIronP = (population, fields, _, _) =>
        {
            int count = (int)(population * IronSynthetic);
            return new ResourceStack(fields.Type, count);
        };
        public static Produce calcAluminumP = (population, fields, _, _) =>
        {
            int count = (int)(population * AluminumSynthetic);
            return new ResourceStack(fields.Type, count);
        };

        public static Produce calcElectronicP = (population, fields, silicon, copper) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * ElectronicSynthetic);
            if (silicon >= expectProduce * Silicon2Elec && copper>= expectProduce * Copper2Elec)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (silicon>= copper)
                {
                    acutalProduce = (int)(silicon/ Silicon2Elec);
                }
                else
                {
                    acutalProduce = (int)(copper/ Copper2Elec);
                }
                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static Produce calcIndustrailP = (population, fields, iron, aluminum) =>
        {
            int expectProduce = (int)(Math.Min(population, maxAllocation * fields.Count) * IndustrailSynthetic);

            if (iron>= expectProduce * Iron2Indus && aluminum>= expectProduce * Aluminum2Indus)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (iron>= aluminum)
                {
                    acutalProduce = (int)(iron/ Iron2Indus);
                }
                else
                {
                    acutalProduce = (int)(aluminum/ Aluminum2Indus);
                }
                return new ResourceStack(fields.Type, acutalProduce);
            }
        };

        public static ProductionConsume calcElectronicPC = (electronic) =>
        {
            ResourceStack silicon = new ResourceStack(GameResourceType.Silicon, (int)(Silicon2Elec * electronic.Count));
            ResourceStack coppor = new ResourceStack(GameResourceType.Copper, (int)(Copper2Elec * electronic.Count));
            return (silicon, coppor);
        };

        public static ProductionConsume calcIndustrailPC = (industrail) =>
        {
            ResourceStack iron = new ResourceStack(GameResourceType.Iron, (int)(Iron2Indus * industrail.Count));
            ResourceStack aluminum = new ResourceStack(GameResourceType.Aluminum, (int)(Aluminum2Indus * industrail.Count));
            return (iron, aluminum);
        };

        public static int calcPopGrowthProgress(int popCount, int silicon, int copper, int iron, int aluminum)
        {
            double rate = 0.2;
            double K;

            
            Dictionary<GameResourceType, double> resourceSynthetic = new Dictionary<GameResourceType, double>()
            {
                { GameResourceType.Silicon, SILICON_PER_POP_TICK },
                { GameResourceType.Copper, COPPER_PER_POP_TICK },
                { GameResourceType.Iron, IRON_PER_POP_TICK },
                { GameResourceType.Aluminum, ALUMINUM_PER_POP_TICK }
            };


            double minK = double.MaxValue;
            minK = Math.Min(minK, silicon / resourceSynthetic[GameResourceType.Silicon]);
            minK = Math.Min(minK, copper / resourceSynthetic[GameResourceType.Copper]);
            minK = Math.Min(minK, iron / resourceSynthetic[GameResourceType.Iron]);
            minK = Math.Min(minK, aluminum / resourceSynthetic[GameResourceType.Aluminum]);

            K = minK;
            if (K > 5e8) { 
                K = 5e8;
            }
            else if (K < 1) {
                K = 1;
            }

            double popGrowth = popCount * rate * (1 - (double)popCount / K);

            return (int)popGrowth * 100;
        }

        public static ArmyPrduce BattleArmyproduce = (requiredArmy) =>
        {
            return ((int)(requiredArmy.Count * BattlePopSynthetic), ResourceStack.EMPTY);
        };

        public static ArmyPrduce produceInfomativeArmy = (requiredArmy) =>
        {
            return ((int)(requiredArmy.Count * InformativePopSynthetic),
            new ResourceStack(GameResourceType.Electronic, requiredArmy.Count * InformativeResourceSynthetic));
        };

        public static ArmyPrduce produceMechanismArmy = (requiredArmy) =>
        {
            return ((int)(requiredArmy.Count * MechanismPopSynthetic),
            new ResourceStack(GameResourceType.Industrial, requiredArmy.Count * MechanismResourceSynthetic));
        };

    }
}
