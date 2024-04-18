using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.TreatyPacket
{
    public class BreakTreatyPacket : IPacket<BreakTreatyPacket>
    {
        private string target;
        public BreakTreatyPacket(string target)
        {
            this.target = target;
        }

        public static BreakTreatyPacket Decode(BinaryReader reader)
        {
            return new BreakTreatyPacket(reader.ReadString());
        }

        public static void Encode(BreakTreatyPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.target);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                IPlayer targetPlayer = server.GetPlayer(target)!;
                List<ITreaty> removeTreaty = new List<ITreaty>();
                foreach(ITreaty treaty in server.PlayerList.TreatyManager.RelationTreatyList)
                {
                    if(treaty.FirstParty == player && treaty.SecondParty == targetPlayer)
                    {
                        removeTreaty.Add(treaty);
                    }
                    if(treaty.FirstParty == targetPlayer && treaty.SecondParty == player)
                    {
                        removeTreaty.Add(treaty);
                    }
                }
                for (int i = 0; i < removeTreaty.Count; i++)
                {
                    server.PlayerList.TreatyManager.RemoveRelationTreaty(removeTreaty[i]);
                }
                ServerMessagePacket frontPacket = new ServerMessagePacket(player.PlayerName + " broke the treaty with you!");
                ServerMessagePacket backPacket = new ServerMessagePacket("You have successfully broken the treaty!");
                targetPlayer.SendPacket(frontPacket);
                player.SendPacket(backPacket);
            }
        }
    }
}
