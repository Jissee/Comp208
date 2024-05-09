namespace EoE.Network.Packets
{
    /// <summary>
    /// A function that writes the packet data to the buffer, which is sent through the network.
    /// </summary>
    /// <typeparam name="T">The type of the packet</typeparam>
    /// <param name="obj">The packet instance</param>
    /// <param name="writer">Packet buffer writer</param>
    public delegate void Encoder<in T>(T obj, BinaryWriter writer);
    /// <summary>
    /// A function that reads data from the packet buffer and constructs the packet.
    /// </summary>
    /// <typeparam name="T">The type of the packet</typeparam>
    /// <param name="reader">Packet buffer reader</param>
    /// <returns>A new instance of the packet</returns>
    public delegate T Decoder<out T>(BinaryReader reader);
    /// <summary>
    /// An interface of packet codecs, implemented by all packets.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPacket<T> : IBasePacket
    {
        abstract static void Encode(T obj, BinaryWriter writer);

        abstract static T Decode(BinaryReader reader);

    }

}
