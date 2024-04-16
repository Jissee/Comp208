using EoE.GovernanceSystem;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem
{
    public struct ResourceStack
    {
        public GameResourceType Type { get; init; }
        private int count;
        public int Count
        { 
            get => count; 
            set 
            {
                if (value < 0)
                {
                    count = 0;
                }
                else
                {
                    count = value;
                }
            }
        }
        public static readonly ResourceStack EMPTY = new ResourceStack(GameResourceType.None, 0);
        public ResourceStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

       

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param invitorName="adder"></param>
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

        public override string ToString()
        {
            return $"[{Type}/{Count}]";
        }

        public static Encoder<ResourceStack> encoder = (ResourceStack obj, BinaryWriter writer) =>
        {
            writer.Write((int)obj.Type);
            writer.Write(obj.Count);
        };

        public static Decoder<ResourceStack> decoder = (BinaryReader reader) =>
        {
            int type = (reader.ReadInt32());
            int count = reader.ReadInt32();
            return new ResourceStack((GameResourceType)type, count);
        };
    }
}
