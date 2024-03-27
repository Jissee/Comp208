using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
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
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static FieldStack operator +(FieldStack target, FieldStack adder)
        {
            if (target.Type != adder.Type)
            {
                throw new Exception("Wrong resource type!");
            }
            else
            {
                target.Count += adder.Count;
                return target;
            }
        }
    }
}
