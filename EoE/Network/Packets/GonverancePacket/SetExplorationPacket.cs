using EoE.Network.Entities;

namespace EoE.Network.Packets.GonverancePacket
{
    public class SetExplorationPacket : IPacket<SetExplorationPacket>
    {
        private int explorationPop;

        public SetExplorationPacket(int explorationPop)
        {
            this.explorationPop = explorationPop;
        }
        public static SetExplorationPacket Decode(BinaryReader reader)
        {
            return new SetExplorationPacket(reader.ReadInt32());
        }

        public static void Encode(SetExplorationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.explorationPop);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer player = context.PlayerSender;
                    player.GonveranceManager.SetExploration(explorationPop);
                }
            }
        }
    }
}
