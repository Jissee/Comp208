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
        private const int inilRes = 100; // initial resources contain
        private const int inilPupolation = 18;// initial population
        private const int normField = 10;  // initial field contain
        private const int richField = 20; // initial rich field contain
        private const int secNormRate = 2; // Secondary resource produce rate
        private const int secRichRate = 4; // Secondary resource rich produce rate



        private static int resProductRate = 10;  //Resource produce rate
        private static int resConsumeRate = 9; //Consume rate
        private static int secondaryResConsumeRate = 2;
        private static float popExGrowth = 1.5F;
        private static float popModGrowth = 1.1F;
        private static float  popDecrease = 0.5F;
        private static int fieldMaxAllocation = 50;

        private Dictionary<ResourcesType, int> resContain;
        private Dictionary<ResourcesType, int> popAllocation;
        private Dictionary<ResourcesType, int> secondaryResGenerateRate;
       // private Dictionary<ResourcesType, int> secondaryResConsumeRate;//Synthetic consumption
        private Dictionary<FieldsType, int> fieldContain;
        public int Population { get; private set; }
 
        

        public PlayerResource(ResourcesType RichRes)
        {
            bool flag = false;
            Population = inilPupolation;

            // initialize resource contain 
            resContain = new Dictionary<ResourcesType, int>();
            foreach (ResourcesType res in Enum.GetValues(typeof(ResourcesType)))
            {
                resContain.Add(res, inilRes);
            }

            // initialize population allocation 
            popAllocation = new Dictionary<ResourcesType, int>();
            foreach (ResourcesType res in Enum.GetValues(typeof(ResourcesType)))
            {
                popAllocation.Add(res, 0);
            }

            // initialize field contian
            fieldContain = new Dictionary<FieldsType, int>();
            // get string type rich resource
            String rich =  RichRes.ToString();

            foreach (FieldsType field in Enum.GetValues(typeof(FieldsType)))
            { 
                String thisField = field.ToString();

                if (rich == thisField)
                {
                    fieldContain.Add(field, richField);
                    flag = true;
                }
                else
                {
                    fieldContain.Add(field, normField);
                }
                
            }

            //initialize secondary product synthetic ratio
            secondaryResGenerateRate = new Dictionary<ResourcesType, int>();

            if (flag)
            {
                secondaryResGenerateRate.Add(ResourcesType.IndustrialProduct, secNormRate);
                secondaryResGenerateRate.Add(ResourcesType.ElectronicProduct, secNormRate);
            }else
            {
                if (rich == "Electronic Product")
                {
                    secondaryResGenerateRate.Add(ResourcesType.IndustrialProduct, secNormRate);
                    secondaryResGenerateRate.Add(ResourcesType.ElectronicProduct, secRichRate);
                }else
                {
                    secondaryResGenerateRate.Add(ResourcesType.IndustrialProduct, secRichRate);
                    secondaryResGenerateRate.Add(ResourcesType.ElectronicProduct, secNormRate);
                }
            }

        }

        /// <summary>
        /// Get one resourcce contain
        /// </summary>
        /// <param name="resources">The name of the resource</param>
        /// <returns>The holding </returns>
        public int GetResContain(ResourcesType resources)
        {
            return resContain [resources];
        }

        /// <summary>
        /// Get all resourcce contain
        /// </summary>
        /// <param name="all">Should be "ALL"</param>
        /// <returns>Dictionary <ResourcesType, int>, all resource and contain</returns>
        public bool GetResContain(string all, out Dictionary<ResourcesType, int> result) 
        {
            if (all =="ALL")
            {
                result = resContain;
                return true;
            }
            else
            {
                result = new Dictionary<ResourcesType, int>();
                return false;
            }
        }

        /// <summary>
        /// Get population allocation
        /// </summary>
        /// <param name="all">Should be "ALL"</param>
        /// <returns>Dictionary <ResourcesType, int>, all resource and population allocated</returns>
        public bool GetpPopAllocation(string all, out Dictionary<ResourcesType, int> result)
        {
            if (all == "ALL")
            {
                result = popAllocation;
                return true;
            }
            else
            {
                result = new Dictionary<ResourcesType, int>();
                return false;
            }
        }

        public bool SetPopAllocation(Dictionary<ResourcesType, int> newAllocation)
        {
            int count = 0;
            foreach (ResourcesType res in Enum.GetValues(typeof(ResourcesType)))
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
                foreach (ResourcesType res in Enum.GetValues(typeof(ResourcesType)))
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
                resContain[(ResourcesType)j] += totalOutPut[j];
            }

            bool rich = true;
            bool sufficient = true;
            int i = 0;
            int shortageAmount = 0;

            // produce more than synthetic consumption
            if (totalOutPut[6] == -2)
            {
                foreach (FieldsType field in Enum.GetValues(typeof(FieldsType)))
                {
                    ResourcesType res;
                    bool success = Enum.TryParse<ResourcesType>(field.ToString(), out res);
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
                
                foreach (FieldsType field in Enum.GetValues(typeof(FieldsType)))
                {
                    ResourcesType res;
                    bool success = Enum.TryParse<ResourcesType>(field.ToString(), out res);
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
                foreach (FieldsType field in Enum.GetValues(typeof(FieldsType)))
                {
                    ResourcesType res;
                    bool success = Enum.TryParse<ResourcesType>(field.ToString(), out res);
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
            foreach (FieldsType field in Enum.GetValues(typeof(FieldsType)))
            {
                String thisField = field.ToString();
                bool flag = Enum.TryParse<ResourcesType>(thisField, out ResourcesType result);
                if (flag)
                {
                    workers = popAllocation[result];
                    // Check if allocate more than max
                    int maxAllocation = fieldMaxAllocation * fieldContain[field];
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
                ResourcesType thisResource = (ResourcesType)i;
                workers = popAllocation[thisResource];
                expectedOutPut = workers * secondaryResGenerateRate[thisResource];
                expectedConsume = expectedOutPut * secondaryResConsumeRate;

                if (outPut[j]>= expectedConsume && outPut[j+1] >= expectedConsume)
                {
                    outPut[j] -= expectedConsume;
                    outPut[j+1] -= expectedConsume;
                    cases -= 1;
                }
                else if (resContain[(ResourcesType)j] + outPut[j] >= expectedConsume && resContain[(ResourcesType)(j+1)] + outPut[j+1] >= expectedConsume)
                {
                    outPut[j] -= expectedConsume;
                    outPut[j + 1] -= expectedConsume;
                    cases += 1;
                }
                else
                {
                    // check which synthetic raw material is lesser, base on this calculate actualOutPut
                    actualConsume = (int) MathF.Min((resContain[(ResourcesType)j] + outPut[j]), (resContain[(ResourcesType)(j + 1)] + outPut[j + 1] )) / secondaryResConsumeRate * secondaryResConsumeRate;
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
            PopulationExponentialGrowth();
        }
    }
   
}
