namespace EoE.Network.Packets
{
    /// <summary>
    /// The base interface of packets which defines the callback of the packet.
    /// </summary>
    public interface IBasePacket
    {
        void Handle(PacketContext context);
    }
}
