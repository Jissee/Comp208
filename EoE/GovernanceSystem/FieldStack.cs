using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem
{
    public struct FieldStack
    {
        private GameResourceType type;

        public GameResourceType Type 
        { 
            get => type;
            init 
            {
                if (value == GameResourceType.BattleArmy || value == GameResourceType.InformativeArmy||value == GameResourceType.MechanismArmy)
                {
                    throw new Exception("No such field");
                }
                else
                {
                    type = value;
                }
            }
        }
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

        public FieldStack(GameResourceType type, int count)
        {
            this.Type = type;
            this.Count = count;
        }

        /// <summary>
        /// Add the adder to the target
        /// </summary>
        /// <param invitorName="adder"></param>
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

        public static Encoder<FieldStack> encoder = (FieldStack obj, BinaryWriter writer) =>
        {
            writer.Write((int)obj.Type);
            writer.Write(obj.Count);
        };

        public static Decoder<FieldStack> decoder = (BinaryReader reader) =>
        {
            return new FieldStack((GameResourceType)reader.ReadInt32(), reader.ReadInt32());
        };
    }
}
