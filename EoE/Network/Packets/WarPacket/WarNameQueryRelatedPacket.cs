using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarNameQueryRelatedPacket : IPacket<WarNameQueryRelatedPacket>
    {
        string[] warNames;
        public WarNameQueryRelatedPacket(string[] warNames)
        {
            this.warNames = warNames;
        }

        public static WarNameQueryRelatedPacket Decode(BinaryReader reader)
        {
            int cut = reader.ReadInt32();
            string[] names = new string[cut];
            for (int i = 0; i < cut; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarNameQueryRelatedPacket(names);
        }

        public static void Encode(WarNameQueryRelatedPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warNames.Length);
            for (int i = 0; i < obj.warNames.Length; i++)
            {
                writer.Write(obj.warNames[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                List<string> names = new List<string>();
                foreach (var kvp in server.PlayerList.WarManager.WarDict)
                {
                    if (kvp.Value.Attackers.Armies.Keys.Contains(player) || kvp.Value.Defenders.Armies.Keys.Contains(player))
                    {
                        names.Add(kvp.Value.WarName);
                    }
                }
                WarNameQueryRelatedPacket packet = new WarNameQueryRelatedPacket([.. names]);
                player.SendPacket(packet);
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.ClientWarNameRelatedList.ChangeWarNames(warNames);
            }
        }
    }
}
