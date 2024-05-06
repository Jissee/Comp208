namespace EoE.Network.Packets
{
    public interface IBasePacket
    {
        void Handle(PacketContext context);
    }
}
