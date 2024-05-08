using EoE.Network.Packets;

namespace EoE.Governance
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
            Type = type;
            Count = count;
        }


        public override string ToString()
        {
            return $"[{Type}/{Count}]";
        }

        public static Encoder<ResourceStack> encoder = (obj, writer) =>
        {
            writer.Write((int)obj.Type);
            writer.Write(obj.Count);
        };

        public static Decoder<ResourceStack> decoder = (reader) =>
        {
            int type = reader.ReadInt32();
            int count = reader.ReadInt32();
            return new ResourceStack((GameResourceType)type, count);
        };
    }
}
