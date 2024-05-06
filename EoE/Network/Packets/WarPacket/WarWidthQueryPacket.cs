using EoE.Network.Entities;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarWidthQueryPacket : IPacket<WarWidthQueryPacket>
    {
        private string warName;
        private int width;
        public WarWidthQueryPacket(string warName, int width)
        {
            this.warName = warName;
            this.width = width;
        }

        public static WarWidthQueryPacket Decode(BinaryReader reader)
        {
            return new WarWidthQueryPacket(reader.ReadString(), reader.ReadInt32());
        }

        public static void Encode(WarWidthQueryPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.width);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                if (server.PlayerList.WarManager.WarDict.ContainsKey(warName))
                {
                    int warWidth;
                    IWarParty Attacker = server.PlayerList.WarManager.WarDict[warName].Attackers;
                    IWarParty Defender = server.PlayerList.WarManager.WarDict[warName].Defenders;
                    if (Attacker.Armies.Keys.Contains(player))
                    {
                        warWidth = Attacker.WarWidth;
                    }
                    else 
                    {
                        warWidth = Defender.WarWidth;
                    }
                    WarWidthQueryPacket packet = new WarWidthQueryPacket(warName, warWidth);
                    player.SendPacket(packet);
                }
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.WarManager.ClientWarWidthList.ChangeWarWidth(warName, width);
            }
        }
    }
}
