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

        public static Recipie produceSilicon = (input, fields, _, _) =>
        {
            return null;
        };
        public static Recipie produceCopper = (input, fields, _, _) =>
        {
            return null;
        }; 
        public static Recipie produceIron = (input, fields, _, _) =>
        {
            return null;
        }; 
        public static Recipie produceAluminium = (input, fields, _, _) =>
        {
            return null;
        };
        public static Recipie produceElectronic = (input, fields, i1, i2) =>
        {

            return null;
        };
        public static Recipie produeIndustrial = (input, fields, i1, i2) =>
        {

            return null;
        };



    }
}
