using EoE.Network.Entities;

namespace EoE.Network.Packets.WarPacket
{
    public class WarInformationPacket : IPacket<WarInformationPacket>
    {
        private string warName;
        private int totalBattle;
        private int totalInformative;
        private int totalMechanism;
        private int battleLost;
        private int informativeLost;
        private int mechanismLost;

        private int enemyTotalBattle;
        private int enemyTotalInformative;
        private int enemyTotalMechanism;
        private int enemyBattleLost;
        private int enemyInformativeLost;
        private int enemyMechanismLost;

        public WarInformationPacket(
            string warName,
            int totalBattle,
            int totalInformative,
            int totalMechanism,
            int battleLost,
            int informativeLost,
            int mechanismLost,

            int enemyTotalBattle,
            int enemyTotalInformative,
            int enemyTotalMechanism,
            int enemyBattleLost,
            int enemyInformativeLost,
            int enemyMechanismLost
            )
        {
            this.warName = warName;
            this.totalBattle = totalBattle;
            this.totalInformative = totalInformative;
            this.totalMechanism = totalMechanism;
            this.battleLost = battleLost;
            this.informativeLost = informativeLost;
            this.mechanismLost = mechanismLost;

            this.enemyTotalBattle = enemyTotalBattle;
            this.enemyTotalInformative = enemyTotalInformative;
            this.enemyTotalMechanism = enemyTotalMechanism;
            this.enemyBattleLost = enemyBattleLost;
            this.enemyInformativeLost = enemyInformativeLost;
            this.enemyMechanismLost = enemyMechanismLost;
        }
        public static WarInformationPacket Decode(BinaryReader reader)
        {
            return new WarInformationPacket(
                reader.ReadString(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32()
            );
        }

        public static void Encode(WarInformationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.totalBattle);
            writer.Write(obj.totalInformative);
            writer.Write(obj.totalMechanism);
            writer.Write(obj.battleLost);
            writer.Write(obj.informativeLost);
            writer.Write(obj.mechanismLost);

            writer.Write(obj.enemyTotalBattle);
            writer.Write(obj.enemyTotalInformative);
            writer.Write(obj.enemyTotalMechanism);
            writer.Write(obj.enemyBattleLost);
            writer.Write(obj.enemyInformativeLost);
            writer.Write(obj.enemyMechanismLost);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == Entities.NetworkDirection.Server2Client)
            {
                IClient client = (IClient)context.Receiver;
                client.WarManager.ClientWarInformationList.ChangeWarInformationList(
                    warName,
                    totalBattle,
                    totalInformative,
                    totalMechanism,
                    battleLost,
                    informativeLost,
                    mechanismLost,

                    enemyTotalBattle,
                    enemyTotalInformative,
                    enemyTotalMechanism,
                    enemyBattleLost,
                    enemyInformativeLost,
                    enemyMechanismLost
                    );
            }
        }
    }
}
