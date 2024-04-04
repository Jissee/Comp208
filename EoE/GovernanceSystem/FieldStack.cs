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

        /// <summary>
        /// Subtract filed
        /// </summary>
        /// <param name="field"></param>
        /// <exception cref="Exception"></exception>
        public void Sub(FieldStack field)
        {
            if (this.Type != field.Type)
            {
                throw new Exception("Wrong resource type!");
            }
            else if (field.Count > Count)
            {
                throw new Exception("Try to subtract a larger FieldStack !");
            }
            else
            {
                this.Count -= field.Count;
            }
        }

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param name="adder"></param>
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

        public FieldStack Split(int count)
        {
            if (this.Count >= count)
            {
                this.Count -= count;
                return new FieldStack(this.Type, count);
            }
            else
            {
                int tmpcnt = this.Count;
                this.Count = 0;
                return new FieldStack(this.Type, tmpcnt);
            }
        }
    }
}
