using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
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
    public static class Recipes
    {
        private static float PrimaryProdcutivity = 1.2f;
        private static int PrimaryComsueRate = 1;
        private static float SecondaryProdcutivity =1.2f;

        public static int SiliconSynthetic = 2;
        public static int CopperSynthetic = 2;
        public static int IronSynthetic = 2;
        public static int AluminumSynthetic = 2;

        public static readonly int THRESHOLD = 10000;

        private static int maxAllocation = 50;

        public static Recipe producePrimaryResource = (population,fields,_,_) =>
        {
            int count = (int)(Math.Max(population, maxAllocation * fields.Count) * PrimaryProdcutivity);
            return new ResourceStack(fields.Type, count);
        };
        public static Recipe produceElectronic = (population, fields, Silicon, Copper) =>
        {
            int expectProduce = (int)(Math.Max(population, maxAllocation * fields.Count) * SecondaryProdcutivity);

            if (Silicon.Count >= expectProduce * SiliconSynthetic && Copper.Count >= expectProduce * CopperSynthetic)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (Silicon.Count >= Copper.Count)
                {
                    acutalProduce = (int)(Silicon.Count / SiliconSynthetic);
                }
                else
                {
                    acutalProduce = (int)(Copper.Count / CopperSynthetic);
                }
                Silicon.Count -= (int)(acutalProduce * SiliconSynthetic);
                Copper.Count -= (int)(acutalProduce * CopperSynthetic);

                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static Recipe produceIndustry = (population, fields, Iron, Aluminum) =>
        {
            int expectProduce = (int)(Math.Max(population, maxAllocation * fields.Count) * SecondaryProdcutivity);

            if (Iron.Count >= expectProduce * IronSynthetic && Aluminum.Count >= expectProduce * AluminumSynthetic)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (Iron.Count >= Aluminum.Count)
                {
                    acutalProduce = (int)(Iron.Count / IronSynthetic);
                }
                else
                {
                    acutalProduce = (int)(Aluminum.Count / AluminumSynthetic);
                }
                Iron.Count -= (int)(acutalProduce * IronSynthetic);
                Aluminum.Count -= (int)(acutalProduce * AluminumSynthetic);

                return new ResourceStack(fields.Type, acutalProduce);
            }

        };

        public static PopModel calcPopGrowthProgress = (int population, int surplus ) =>
        {
            if (surplus >= THRESHOLD)
            {
                population *= 2;
                // to do exposion growth
            }
            else if (surplus >= 0)
            {
                population = (int)(population * 0.1f);
                // to do smooth growth
            }
            else if (surplus < 0)
            {
                population = -(int)(population * 0.1f);
                // to do smooth decrease
            }
            else if(surplus <= -THRESHOLD)
            {
                population = -(int)(population * 2.0f);
                // to do exposion decrease
            }

            return population;
        };
        [Obsolete]
    }
}
