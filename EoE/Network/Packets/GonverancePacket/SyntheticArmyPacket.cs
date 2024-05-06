using EoE.GovernanceSystem;
using EoE.Network.Entities;

namespace EoE.Network.Packets.GonverancePacket
{
    public class SyntheticArmyPacket : IPacket<SyntheticArmyPacket>
    {
        private GameResourceType type;
        private int count;

        public SyntheticArmyPacket(GameResourceType type, int count)
        {
            if ((int)type < (int)GameResourceType.BattleArmy)
            {
                throw new Exception("should be an army");
            }
            this.type = type;
            this.count = count;
        }
        public static SyntheticArmyPacket Decode(BinaryReader reader)
        {
            return new SyntheticArmyPacket((GameResourceType)reader.ReadByte(), reader.ReadInt32());
        }

        public static void Encode(SyntheticArmyPacket obj, BinaryWriter writer)
        {
            writer.Write((byte)obj.type);
            writer.Write(obj.count);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer player = context.PlayerSender;
                    player.GonveranceManager.SyntheticArmy(type, count);
                }
            }
        }
    }
}
