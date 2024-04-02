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

        public ResourceStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

        public void Sub(ResourceStack resource)
        {
            if (this.Type != resource.Type)
            {
                throw new Exception("Wrong resource type!");
            }
            else if (resource.Count > Count)
            {
                throw new Exception("Try to subtract a larger ResourceStack !");
            }
            else
            {
                this.Count -= resource.Count;
            }
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

        public ResourceStack Split(ResourceStack spliter)
        {
            if (this.Type != spliter.Type)
            {
                throw new Exception("Wrong resource type!");
            }
            if (this.Count >= spliter.Count)
            {
                this.Sub(spliter);
                return spliter;
            }
            else
            {
                int count = this.Count;
                this.Count = 0;
                return new ResourceStack(this.Type, count);
            }
        }
    }
}
