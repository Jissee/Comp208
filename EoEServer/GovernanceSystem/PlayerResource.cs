using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerResource : ITickable
    {
        private readonly int INIT_RESOURCE = 100; // initial resources contain
        private readonly int INIT_POPULATION = 18;// initial population
        private readonly int NORM_FIELD = 10;  // initial field contain
        private readonly int RICH_FIELD = 20; // initial rich field contain
        private readonly int SecResProduceRate = 2; // Secondary resource produce rate
        private readonly int secRichRate = 4; // Secondary resource rich produce rate



        private static int resProductRate = 10;  //Resource produce rate
        private static int resConsumeRate = 9; //Consume rate
        private static int secondaryResConsumeRate = 2;
        private static float popExGrowth = 1.5F;
        private static float popModGrowth = 1.1F;
        private static float  popDecrease = 0.5F;
        private static int fieldMaxAllocation = 50;

        private ResourceStack si;
        private ResourceStack copper;
        private ResourceStack iron;
        private ResourceStack aluminum;

        // private Dictionary<ResourcesType, int> secondaryResConsumeRate;//Synthetic consumption
        private int [] fieldsContain;
        public int AvailablePopulation { get; private set; }
 
        public PlayerResource(GameResourceType RichRes)
        {
            bool flag = false;
            AvailablePopulation = INIT_POPULATION;

            // initialize resource contain 
            resContain = new int [6];
            for (int i = 0; i < resContain.Length; i++)
            {
                resContain[i] = INIT_RESOURCE;
            }

            // initialize population allocation 
            popAllocation = new int[6];
            for (int i = 0; i < popAllocation.Length; i++)
            {
                popAllocation[i] = 0;
            }

            // initialize field contian
            fieldsContain = new int[6];
            // get string type rich resource
            int rich =  (int)RichRes;

            for (int i = 0; i < fieldsContain.Length; i++)
            {
                if (i == rich)
                {
                    fieldsContain[i] = RICH_FIELD;
                }
            }

            //initialize secondary product synthetic ratio
            secondaryResGenerateRate = new Dictionary<GameResourceType, int>();

            if (flag)
            {
                secondaryResGenerateRate.Add(GameResourceType.Industrial, SecResProduceRate);
                secondaryResGenerateRate.Add(GameResourceType.Electronic, SecResProduceRate);
            }else
            {
                if (rich == "Electronic Product")
                {
                    secondaryResGenerateRate.Add(GameResourceType.Industrial, SecResProduceRate);
                    secondaryResGenerateRate.Add(GameResourceType.Electronic, secRichRate);
                }else
                {
                    secondaryResGenerateRate.Add(GameResourceType.Industrial, secRichRate);
                    secondaryResGenerateRate.Add(GameResourceType.Electronic, SecResProduceRate);
                }
            }

        }

        /// <summary>
        /// Get one resourcce contain
        /// </summary>
        /// <param name="resources">The name of the resource</param>
        /// <returns>The holding </returns>
        public int GetResContain(GameResourceType resources)
        {
            return resContain [resources];
        }

        /// <summary>
        /// Get all resourcce contain
        /// </summary>
        /// <returns>Dictionary <ResourcesType, int>, all resource and contain</returns>
        public Dictionary<GameResourceType, int> GetResContain() 
        {
              return resContain;
        }

        /// <summary>
        /// Get population allocation
        /// </summary>
        /// <returns>Dictionary <ResourcesType, int>, all resource and population allocated</returns>
        public Dictionary<GameResourceType, int> GetpPopAllocation()
        {
            return popAllocation;
        }

        /// <summary>
        ///Get total population
        /// </summary>
        /// <returns>A int represent total population</returns>
        public int GetTotalAllocation()
        {
            int totalPopulation = 0;
            foreach (GameResourceType res in Enum.GetValues(typeof(GameResourceType)))
            {
                totalPopulation += popAllocation[res];
            }
            return totalPopulation;

        }

        /// <summary>
        /// Set population allocation
        /// </summary>
        /// <param name="newAllocaation"> input a dictionary represent new population allocation <parameter>
        /// <returns>A boolean indicate allocation successed or failed</returns>
        public bool SetPopAllocation(Dictionary<GameResourceType, int> newAllocation)
        {
            int count = 0;
            foreach (GameResourceType res in Enum.GetValues(typeof(GameResourceType)))
            {
                if (newAllocation[res] < 0 )
                {
                    return false;
                }
                count += newAllocation[res];
            }

            if(count > Population)
            {
                return false;
            }else
            {
                foreach (GameResourceType res in Enum.GetValues(typeof(GameResourceType)))
                {
                    popAllocation[res] = newAllocation[res];
                }
                return true;
            }
           
        }

        public void UpdateResources()
        {
            int[] primaryOutPut = new int[4];
            int[] totalOutPut = new int[6];
            int consume = Population * resConsumeRate;

           

            primaryOutPut = ProducePrimaryResources();

            totalOutPut = ProduceSecondaryResources(primaryOutPut);

            for (int j =0; j < 5; j++)
            {
                resContain[(GameResourceType)j] += totalOutPut[j];
            }

            bool rich = true;
            bool sufficient = true;
            int i = 0;
            int shortageAmount = 0;

            // produce more than synthetic consumption
            if (totalOutPut[6] == -2)
            {
                foreach (GameResourceType field in Enum.GetValues(typeof(GameResourceType)))
                {
                    GameResourceType res;
                    bool success = Enum.TryParse<GameResourceType>(field.ToString(), out res);
                    int turnSurplus = totalOutPut[i] - consume;
                    int resourceSurplus = resContain[res] + turnSurplus;
    
                    if (success)
                    {
                        if (turnSurplus >= 0)
                        {
                            resContain[res] += turnSurplus;
                        }
                        else if (resourceSurplus >= 0)
                        {
                            resContain[res] += turnSurplus;
                            rich = false;
                        }
                        else
                        {
                            resContain[res] = 0;
                            rich = false;
                            sufficient = false;
                            shortageAmount += -resourceSurplus;
                        }
                    }else
                    {
                        Debug.WriteLine("can't parse field");//Need deletion
                    }
                    i++;
                }
                


            }
            // produce plus contain more than synthetic consumption
            else if (totalOutPut[6] <= 2)
            {
                
                foreach (GameResourceType field in Enum.GetValues(typeof(GameResourceType)))
                {
                    GameResourceType res;
                    bool success = Enum.TryParse<GameResourceType>(field.ToString(), out res);
                    int resourceSurplus = resContain[res] + totalOutPut[i] - consume;

                    if (success)
                    {
                        if (resourceSurplus >= 0)
                        {
                            resContain[res] = resourceSurplus;
                        }
                        else
                        {
                            resContain[res] = 0;
                            sufficient = false;
                            shortageAmount += -resourceSurplus;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("can't parse field");//Need deletion
                    }

                    i++;
                }
            }
            else if (totalOutPut[6] <= 6)
            {
                foreach (GameResourceType field in Enum.GetValues(typeof(GameResourceType)))
                {
                    GameResourceType res;
                    bool success = Enum.TryParse<GameResourceType>(field.ToString(), out res);
                    int resourceSurplus = resContain[res] + totalOutPut[i] - consume;

                    if (success)
                    {
                        if (resourceSurplus >= 0)
                        {
                            resContain[res] = resourceSurplus;
                        }
                        else
                        {
                            resContain[res] = 0;
                            sufficient = false;
                            shortageAmount += -resourceSurplus;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("can't parse field");//Need deletion
                    }

                    i++;
                }
            }

            // based on the final bool result, updata population
            if (rich)
            {
                PopulationExponentialGrowth();
            }
            else if (sufficient)
            {
                PopulationModerateGrowth();
            }
            else
            {
                PopulationDecrease(shortageAmount);
            }
        }
        
        private int[] ProducePrimaryResources()
        {
            int workers = 0;
            int[] outPut = new int [4];
            int i = 0;
            foreach (GameResourceType field in Enum.GetValues(typeof(GameResourceType)))
            {
                String thisField = field.ToString();
                bool flag = Enum.TryParse<GameResourceType>(thisField, out GameResourceType result);
                if (flag)
                {
                    workers = popAllocation[result];
                    // Check if allocate more than max
                    int maxAllocation = fieldMaxAllocation * fieldsContain[field];
                    if (workers < maxAllocation)
                    {
                        outPut[i] = workers * resProductRate;
                    }else
                    {
                        outPut[i] = maxAllocation * resProductRate; //Calculate ouput using max allocation
                    }
                    i++;
                }
            }
            return outPut;
        }

        private int[] ProduceSecondaryResources(int[] primary)
        {
            const int expectedSize = 4;
            int workers = 0;
            int expectedOutPut = 0;
            int expectedConsume = 0;
            int actualConsume = 0;
            int actualOutPut = 0;
            /* cases = -2 : produce more than synthetic consumption
               -2 <cases <= 2 : produce plus cantian more than synthetic consumption
               2 < cases <= 6 : produce plus cantian more than synthetic consumption
             */
            int cases = 0;
            int[] outPut = new int[7]; //last element will be the cases;
            if (primary.Length != expectedSize)
            {
                throw new ArgumentException($"Array must be exactly {expectedSize} elements long.", nameof(primary));
            }
            for (int i = 0; i < 3; i++)
            {
                outPut[i] = primary[i];
            }
            // enum 3,4 represent two secondary resources
            int j = 0;
            for (int i = 3; i < 4; i++)
            {
                GameResourceType thisResource = (GameResourceType)i;
                workers = popAllocation[thisResource];
                expectedOutPut = workers * secondaryResGenerateRate[thisResource];
                expectedConsume = expectedOutPut * secondaryResConsumeRate;

                if (outPut[j]>= expectedConsume && outPut[j+1] >= expectedConsume)
                {
                    outPut[j] -= expectedConsume;
                    outPut[j+1] -= expectedConsume;
                    cases -= 1;
                }
                else if (resContain[(GameResourceType)j] + outPut[j] >= expectedConsume && resContain[(GameResourceType)(j+1)] + outPut[j+1] >= expectedConsume)
                {
                    outPut[j] -= expectedConsume;
                    outPut[j + 1] -= expectedConsume;
                    cases += 1;
                }
                else
                {
                    // check which synthetic raw material is lesser, base on this calculate actualOutPut
                    actualConsume = (int) MathF.Min((resContain[(GameResourceType)j] + outPut[j]), (resContain[(GameResourceType)(j + 1)] + outPut[j + 1] )) / secondaryResConsumeRate * secondaryResConsumeRate;
                    outPut[j] -= actualConsume;
                    outPut[j+1] -= actualConsume;

                    actualOutPut = actualConsume / secondaryResConsumeRate;
                    outPut[i + 1] = actualOutPut;
                    cases += 3;
                }
                j += 2;
            }
            outPut[6] = cases;
            return outPut;
        }

        private void PopulationExponentialGrowth()
        {
            Population = (int) (Population * popExGrowth);
        }

        private void PopulationModerateGrowth()
        {
            Population = (int)(Population * popModGrowth);
        }

        private void PopulationDecrease(int shortageAmount)
        {
            Population -= (int)(shortageAmount * popDecrease);
        }

        public void Tick()
        {
            UpdateResources();
        }
    }
   
}
