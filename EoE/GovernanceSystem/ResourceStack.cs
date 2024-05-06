using EoE.Network.Packets;

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
            int type = reader.ReadInt32();
            int count = reader.ReadInt32();
            return new ResourceStack((GameResourceType)type, count);
        };
    }
}
