using System.Text;

namespace EoE.GovernanceSystem
{
    public class Modifier
    {
        private string nodeName;
        private Dictionary<string, double> values;
        private List<Modifier> nodes;
        private ModifierType type;
        public Modifier(string nodeName, ModifierType type)
        {
            nodes = new List<Modifier>();
            values = new Dictionary<string, double>();
            this.nodeName = nodeName;
            this.type = type;
        }

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var value in values)
            {
                sb.Append("  ");
                sb.Append(value.Key);
                sb.Append("  ");
                sb.Append(Fmt(value.Value));
                sb.Append("\n");
            }
            foreach (var node in nodes)
            {
                string subValues = node.GetString();
                string[] items = subValues.Split('\n');
                foreach (var item in items)
                {
                    sb.Append("  ");
                    sb.Append(item);
                    sb.Append("\n");
                }
            }
            string symbol;
            if (type == ModifierType.Plus)
            {
                symbol = "+";
            }
            else
            {
                symbol = "*";
            }
            return $"{symbol}{nodeName}  {Fmt(GetValue())} \n{sb}";
        }
        private string Fmt(double value)
        {
            return $"{(value > 0 ? "+" : "")}{value.ToString("#.###")}%";
        }
        public Modifier AddNode(Modifier modifier)
        {
            if (CheckNode(modifier))
            {
                nodes.Add(modifier);
            }
            else
            {
                throw new Exception("Node exists");
            }
            return this;
        }
        private bool CheckNode(Modifier modifier)
        {
            if (modifier == this) return false;
            foreach (var node in nodes)
            {
                if (node == modifier) return false;
                node.CheckNode(modifier);
            }
            foreach (var node in modifier.nodes)
            {
                if (node == this) return false;
                node.CheckNode(this);
            }
            return true;
        }
        public void RemoveNodeAt(int index)
        {
            nodes.RemoveAt(index);
        }
        public Modifier AddValue(string label, double percentage)
        {
            values.Add(label, percentage);
            return this;
        }
        public void RemoveValue(string label)
        {
            values.Remove(label);
        }
        public double GetValue()
        {
            double baseValue = 0;
            if (type == ModifierType.Plus)
            {
                baseValue = 0;
                foreach (var value in values)
                {
                    baseValue += value.Value;
                }
                foreach (var node in nodes)
                {
                    baseValue += node.GetValue();
                }
            }
            else if (type == ModifierType.Times)
            {
                baseValue = 100;
                foreach (var value in values)
                {
                    baseValue *= 1 + (value.Value / 100);
                }
                foreach (var node in nodes)
                {
                    baseValue *= 1 + (node.GetValue() / 100);
                }
                baseValue -= 100;
            }
            return baseValue;
        }
        public double Apply(double value)
        {
            return value * (1 + GetValue() / 100);
        }

        public Modifier this[int index]
        {
            get
            {
                return nodes[index];
            }
        }
        public enum ModifierType
        {
            Plus,
            Times
        }
    }
}
