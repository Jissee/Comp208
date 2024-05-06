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
        public T GetAndRemove()
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
            T element = elements[i];
            RemoveAt(i);
            return element;
        }
        public T GetAndDecreaseOne()
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
            T element = elements[i];
            weights[i]--;
            totalWeight--;
            return element;
        }
    }
}
