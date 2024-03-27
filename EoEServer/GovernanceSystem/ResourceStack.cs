using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.GovernanceSystem
{
    public class ResourceStack
    {
        public GameResourceType Type { get; init; }
        public int Count { get; set; }

        public ResourceStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

        public ResourceStack Split(int count)
        {
            if(count > Count)
            {
                return this;
            }
            else
            {
                this.Count -= count;
                return new ResourceStack(Type, count);
            }
        }

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="adder"></param>
        /// <returns>return the target</returns>
        /// <exception cref="Exception"></exception>
        public void Add(ResourceStack adder)
        {
            if(this.Type != adder.Type)
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
