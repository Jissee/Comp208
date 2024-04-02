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
        float produceRate,
        FieldStack producingFields,
        ResourceStack? input1,
        ResourceStack? input2,
        int? input1Synthetic,
        int? input2Synthetic
    );
    public static class Recipies
    {
        private static int maxAllocation = 50;

        public static Recipie producePrimaryResource = (population, produceRate,fields,_,_,_,_) =>
        {
            int count = (int)(Math.Max(population, maxAllocation * fields.Count) * produceRate);
            return new ResourceStack(fields.Type, count);
        };
        public static Recipie produceSecondaryResource = (population, produceRate, fields, consumable1, consumable2, input1Synthetic, input2Synthetic) =>
        {
            int expectProduce = (int)(Math.Max(population, maxAllocation * fields.Count) * produceRate);

            if (consumable1.Count >= expectProduce * input1Synthetic && consumable2.Count >= expectProduce * input2Synthetic)
            {
                return new ResourceStack(fields.Type, expectProduce);
            }
            else
            {
                int acutalProduce = 0;
                if (consumable1.Count >= consumable2.Count)
                {
                    acutalProduce = (int)(consumable1.Count / input1Synthetic);
                }
                else
                {
                    acutalProduce = (int)(consumable2.Count / input2Synthetic);
                }
                consumable1.Count -= (int)(acutalProduce * input1Synthetic);
                consumable2.Count -= (int)(acutalProduce * input2Synthetic);

                return new ResourceStack(fields.Type, acutalProduce);
            }

        }; 

    }
}
