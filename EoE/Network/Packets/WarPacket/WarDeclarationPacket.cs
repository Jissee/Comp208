using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.War;
using EoE.War.Interface;
using System.Text;

namespace EoE.Network.Packets.WarPacket
{
    public class WarDeclarationPacket : IPacket<WarDeclarationPacket>
    {

        private string warName;
        public WarDeclarationPacket(string warName)
        {
            this.warName = warName;
        }
        public static WarDeclarationPacket Decode(BinaryReader reader)
        {
            return new WarDeclarationPacket(reader.ReadString());
        }

        public static void Encode(WarDeclarationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                IWarManager manager = server.PlayerList.WarManager;

                if (manager.PreparingWarDict.ContainsKey(warName))
                {
                    if (!manager.PreparingWarDict[warName].Attackers.Contains(player))
                    {
                        manager.PreparingWarDict[warName].Attackers.AddPlayer(player);
                    }
                }

                manager.DeclareWar(warName);

                IWar war = server.PlayerList.WarManager.WarDict[warName];
                IWarParty attackers = war.Attackers;
                IWarParty defenders = war.Defenders;

                WarTarget attackersTarget = new WarTarget();
                WarTarget defendersTarget = new WarTarget();
                StringBuilder stringBuilderAttacker = new StringBuilder();
                StringBuilder stringBuilderDefender = new StringBuilder();
                foreach (IPlayer attacker in attackers.Armies.Keys)
                {
                    stringBuilderAttacker.Append($" \"{attacker.PlayerName}\" ");
                }
                foreach (IPlayer defender in defenders.Armies.Keys)
                {
                    stringBuilderDefender.Append($" \"{defender.PlayerName}\" ");
                }
                ServerMessagePacket attackerPacket = new ServerMessagePacket(
                    $"""
                    You will have a war with the following players:
                    {stringBuilderDefender.ToString()}
                    You will have these alliances:
                    {stringBuilderAttacker.ToString()}
                    Please Remember to fill in the frontier!
                    Otherwise, you will automatically surrender!
                    """);
                ServerMessagePacket defenderPacket = new ServerMessagePacket(
                    $"""
                    You will have a war with the following players:
                    {stringBuilderAttacker.ToString()}
                    You will have these alliances:
                    {stringBuilderDefender.ToString()}
                    Please Remember to fill in the frontier!
                    Otherwise, you will automatically surrender!
                    """);
                server.PlayerList.Broadcast(attackerPacket, player => attackers.Armies.ContainsKey(player));
                server.PlayerList.Broadcast(defenderPacket, player => defenders.Armies.ContainsKey(player));
                foreach (IPlayer attacker in attackers.Armies.Keys)
                {
                    foreach (IPlayer defender in defenders.Armies.Keys)
                    {
                        WarTarget target1 = manager.WarTargets[attacker][defender];
                        WarTarget target2 = manager.WarTargets[defender][attacker];
                        attackersTarget.Add(target1);
                        defendersTarget.Add(target2);
                    }
                }
            }
        }
    }
}
