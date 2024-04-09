using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EoE.Util
{
    public class ProbabilityList<T>
    {
        private static Random random = new Random();
        private List<T> elements;
        private List<int> weights;
        private int totalWeight;
        public ProbabilityList()
        {
            elements = new List<T>();
            weights = new List<int>();
            totalWeight = 0;
        }

        public void Add(T element, int weight)
        {
            elements.Add(element);
            weights.Add(weight);
            totalWeight += weight;
        }

        public void RemoveAt(int index)
        {
            totalWeight -= weights[index];
            weights.RemoveAt(index);
            elements.RemoveAt(index);
        }

        public T Get()
        {
            double randomDouble = random.NextDouble();
            int i = 0;
            for (; i < elements.Count; i++)
            {
                double prob = (double)weights[i] / (double)totalWeight;
                randomDouble -= prob;
                if (randomDouble < 0)
                {
                    break;
                }
            }
            return elements[i];
        }
    }
}
