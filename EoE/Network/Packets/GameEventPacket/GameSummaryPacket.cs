using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class GameSummaryPacket : IPacket<GameSummaryPacket>
    {
        // name, resource score, field score, pop score, army score, total score
        private List<(string, int, int, int, int, int)> ranks;
        public GameSummaryPacket(List<(string, int, int, int, int, int)> ranks)
        {
            this.ranks = ranks;
        }
        public static GameSummaryPacket Decode(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            List<(string, int, int, int, int, int)> ranks = new List<(string, int, int, int, int, int)>();
            for (int i = 0; i < length; i++)
            {
                ranks.Add((
                    reader.ReadString(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32()
                    ));
            }
            return new GameSummaryPacket(ranks);
        }

        public static void Encode(GameSummaryPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.ranks.Count);
            foreach (var (name, resourceScore, fieldScore, popScore, armyScore, totalScore) in obj.ranks)
            {
                writer.Write(name);
                writer.Write(resourceScore);
                writer.Write(fieldScore);
                writer.Write(popScore);
                writer.Write(armyScore);
                writer.Write(totalScore);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Server2Client)
            {
                IClient client = (IClient)context.Receiver;
                StringBuilder sb = new StringBuilder();
                int i = 0;
                sb.AppendLine("Rank\t\t\tName\t\t\tResource\t\t\tPopulation\t\t\tArmy\t\t\tTotal");
                foreach (var (name, resourceScore, fieldScore, popScore, armyScore, totalScore) in ranks)
                {
                    i++;
                    sb.AppendLine($"{i}\t\t\t{name}\t\t\t{resourceScore}\t\t\t{fieldScore}\t\t\t{popScore}\t\t\t{armyScore}\t\t\t{totalScore}");
                }
                
                client.MsgBox($"""
                    Game Finished!
                    {sb.ToString()}
                    """);
            }
        }
    }
}
