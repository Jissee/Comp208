using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem
{
    public class ResourceStack
    {
        public GameResourceType Type { get; init; }
        public int Count { get; set; }
        public static readonly ResourceStack EMPTY = new ResourceStack(GameResourceType.None, 0);
        public ResourceStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

       

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param name="adder"></param>
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

        public ResourceStack Split(int count)
        {
            if (this.Count >= count)
            {
                this.Count -= count;
                return new ResourceStack(this.Type, count);
            }
            else
            {
                int tmpcnt = this.Count;
                this.Count = 0;
                return new ResourceStack(this.Type, tmpcnt);
            }
        }
    }
}
