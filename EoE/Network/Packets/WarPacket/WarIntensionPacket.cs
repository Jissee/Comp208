using EoE.Network.Entities;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarIntensionPacket : IPacket<WarIntensionPacket>
    {
        private string warName;
        private string targetPlayerName;
        private List<string> protectors;
        private List<string> participatible;
        public WarIntensionPacket(string warName, string targetPlayerName, string[] protectors, string[] participatible)
        {
            this.warName = warName;
            this.targetPlayerName = targetPlayerName;
            this.protectors = [.. protectors];
            this.participatible = [.. participatible];
        }
        public static WarIntensionPacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            string targetPlayerName = reader.ReadString();

            int protectorCount = reader.ReadInt32();
            List<string> protectors = new List<string>();
            for (int i = 0; i < protectorCount; i++)
            {
                protectors.Add(reader.ReadString());
            }

            int participatibleCount = reader.ReadInt32();
            List<string> participatible = new List<string>();
            for (int i = 0; i < participatibleCount; i++)
            {
                participatible.Add(reader.ReadString());
            }

            return new WarIntensionPacket(warName, targetPlayerName, [.. protectors], [.. participatible]);
        }

        public static void Encode(WarIntensionPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.targetPlayerName);
            writer.Write(obj.protectors.Count);
            foreach(string protector in obj.protectors)
            {
                writer.Write(protector);
            }
            writer.Write(obj.participatible.Count);
            foreach (string participatible in obj.participatible)
            {
                writer.Write(participatible);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer targetPlayer = server.GetPlayer(targetPlayerName)!;
                List<IPlayer> protectors = server.PlayerList.GetProtectorsRecursively(targetPlayer);
                server.PlayerList.WarManager.PreparingWarDict[warName].Attackers.Clear();
                server.PlayerList.WarManager.PreparingWarDict[warName].Defenders.Clear();
                server.PlayerList.WarManager.PreparingWarDict[warName].Defenders.AddPlayer(targetPlayer);
                foreach (IPlayer player in protectors)
                {
                    server.PlayerList.WarManager.PreparingWarDict[warName].Defenders.AddPlayer(player);
                }
                var protectorsEnum = from protector in protectors
                               select protector.PlayerName;

                List<IPlayer> allPlayers = [.. server.PlayerList.Players];
                allPlayers.RemoveAll(protectors.Contains);
                allPlayers.Remove(context.PlayerSender!);
                allPlayers.Remove(server.GetPlayer(targetPlayerName)!);

                var participatibleEnum = from participatable in allPlayers
                                         select participatable.PlayerName;

                WarIntensionPacket packet = new WarIntensionPacket(warName, targetPlayerName, protectorsEnum.ToArray(), participatibleEnum.ToArray());
                context.PlayerSender!.SendPacket(packet);
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.ClientWarProtectorsList.ChangeWarProtectorsList(targetPlayerName, protectors.ToArray());
                client.ClientWarParticipatibleList.ChangeWarPaticipatorsList(targetPlayerName, participatible.ToArray());
                client.ClientWarNameList.ChangeWarName(warName);
            }
        }
    }
}
