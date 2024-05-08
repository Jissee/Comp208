using EoE.Network.Packets;

namespace EoE.Governance
{
    public struct FieldStack
    {
        private GameResourceType type;

        public GameResourceType Type
        {
            get => type;
            init
            {
                if ((int)value >= 6)
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
            Type = type;
            Count = count;
        }

        public static Encoder<FieldStack> encoder = (obj, writer) =>
        {
            writer.Write((int)obj.Type);
            writer.Write(obj.Count);
        };

        public static Decoder<FieldStack> decoder = (reader) =>
        {
            return new FieldStack((GameResourceType)reader.ReadInt32(), reader.ReadInt32());
        };
    }
}
