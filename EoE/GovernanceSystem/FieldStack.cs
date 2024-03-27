using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem
{
    public class FieldStack
    {
        public GameResourceType Type { get; init; }
        public int Count { get; set; }

        public FieldStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

        public FieldStack Split(int count)
        {
            if (count > Count)
            {
                return this;
            }
            else
            {
                this.Count -= count;
                return new FieldStack(Type, count);
            }
        }

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="adder"></param>
        /// <returns>return target</returns>
        /// <exception cref="Exception"></exception>
        public void Add(FieldStack adder)
        {
            if (this.Type != adder.Type)
            {
                throw new Exception("Wrong resource type!");
            }
            else
            {
                this.Count += adder.Count;
            }
        }
    }
}
