using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public delegate void Encoder<in T>(T obj, BinaryWriter writer);
    public delegate T Decoder<out T>(BinaryReader reader);
    public interface IPacket<T> : IBasePacket
    {
        abstract static void Encode(T obj, BinaryWriter writer);

        abstract static T Decode(BinaryReader reader);

    }
    public interface IBasePacket
    {
        void Handle(PacketContext context);
    }
}
