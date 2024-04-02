using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class Productivity
    {
        public float SiliconProdcutivity { get; set; }
        public float CopperProdcutivity{get;set;}
        public float IronProdcutivity{get;set;}
        public float AluminumProdcutivity{get;set;}
        public float PrimaryProductivity { get; set;}

        public float ElectronicProdcutivity{get;set;}
        public float IndustrialProdcutivity{get;set;}

        public float SecondaryProductivity { get; set;}
        public float ProductionBonus { get; set; }

        public int SiliconSynthetic { get; set; }
        public int CopperSynthetic { get; set; }
        public int IronSynthetic { get; set; }
        public int AluminumSynthetic { get; set; }

        public Productivity()
        {
            SiliconProdcutivity = 1.1f;
            CopperProdcutivity = 1.1f;
            IronProdcutivity = 1.1f;
            AluminumProdcutivity = 1.1f;
            PrimaryProductivity = 1.0f;

            ElectronicProdcutivity = 1.1f;
            IndustrialProdcutivity = 1.1f;
            SecondaryProductivity = 1.0f;

            ProductionBonus = 1.0f;

            SiliconSynthetic = 2;
            CopperSynthetic = 2;
            IronSynthetic = 2;
            AluminumSynthetic = 2;
        }

    }
}
