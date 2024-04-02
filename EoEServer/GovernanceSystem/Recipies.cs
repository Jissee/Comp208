using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public delegate ResourceStack Recipie(
        int popCount,
        FieldStack producingFields,
        ResourceStack? input1,
        ResourceStack? input2
    );
    public static class Recipies
    {
        private static float PrimaryProdcutivity = 1.2f;
        private static float SecondaryProdcutivity =1.2f;

        public static int SiliconSynthetic = 2;
        public static int CopperSynthetic = 2;
        public static int IronSynthetic = 2;
        public static int AluminumSynthetic = 2;

        private static int maxAllocation = 50;

        public static Recipie producePrimaryResource = (population,fields,_,_) =>
        {
            int count = (int)(Math.Max(population, maxAllocation * fields.Count) * PrimaryProdcutivity);
            return new ResourceStack(fields.Type, count);
        };
        public static Recipie produceElectronic = (population, fields, Silicon, Copper) =>
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

        public static Recipie produceIndustry = (population, fields, Iron, Aluminum) =>
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

    }
}
