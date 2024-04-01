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
        ResourceStack? input2
    );
    public static class Recipies
    {
        private static int maxAllocation = 50;

        public static Recipie produceSilicon = (input, produceRate,fields,_,_) =>
        {
            int count = (int)(Math.Max(input * fields.Count, maxAllocation * fields.Count) * produceRate);
            return new ResourceStack(fields.Type, count);
        };
        public static Recipie produceCopper = (input, produceRate, fields, _, _) =>
        {
            return null;
        }; 
        public static Recipie produceIron = (input, produceRate, fields, _, _) =>
        {
            return null;
        }; 
        public static Recipie produceAluminium = (input, produceRate, fields, _, _) =>
        {
            return null;
        };
        public static Recipie produceElectronic = (input, produceRat , fields, i1, i2) =>
        {

            return null;
        };
        public static Recipie produeIndustrial = (input, produceRat, fields, i1, i2) =>
        {

            return null;
        };



    }
}
