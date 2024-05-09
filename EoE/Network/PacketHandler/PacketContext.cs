using EoE.Network.Entities;

namespace EoE.Network
{
    /// <summary>
    /// Providing the environment for packet handler.
    /// </summary>
    public class PacketContext
    {
        public PacketContext(NetworkDirection direction, IPlayer? playerSender, INetworkEntity? receiver)
        {
            NetworkDirection = direction;
            PlayerSender = playerSender;
            Receiver = receiver;
        }
        public NetworkDirection NetworkDirection { get; }
        /// <summary>
        /// Only available on server side.
        /// </summary>
        public IPlayer? PlayerSender { get; }
        /// <summary>
        /// Server or Client
        /// </summary>
        public INetworkEntity Receiver { get; }
    }
}
