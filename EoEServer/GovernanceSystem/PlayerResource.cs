using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerResource
    {
        private const int inilRes = 100;
        private static int richResRate;
        private static int resRate;  //Resource produce rate
        private static int csmRate; //Consume rate
        private static int popGrowth;

        private Resources richRes;
        private Dictionary<Resources, int> resContain;
        private Dictionary<Resources, int> popAllocation;
        public int Population { get; private set; }


        public PlayerResource(Resources RichRes)
        {
            this.richRes = RichRes;
            Population = 18;
            resContain = new Dictionary<Resources, int>();
            foreach (Resources value in Enum.GetValues(typeof(Resources)))
            {
                resContain.Add(value, inilRes);
            }

            popAllocation = new Dictionary<Resources, int>();
            foreach (Resources value in Enum.GetValues(typeof(Resources)))
            {
                popAllocation.Add(value, Population / 6);
            }
        }

        public int GetResContain(Resources resources)
        {
            return resContain [resources];
        }

        public int GetpopAllocation(Resources resources)
        {
            return popAllocation[resources];
        }
    }
   
}
