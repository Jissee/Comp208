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
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="adder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ResourceStack operator+(ResourceStack target, ResourceStack adder)
        {
            if(target.Type != adder.Type)
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
