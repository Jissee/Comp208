using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarSurrenderPacket : IPacket<WarSurrenderPacket>
    {
        private string warName;
        public WarSurrenderPacket(string warName)
        {
            this.warName = warName;
        }

        public static WarSurrenderPacket Decode(BinaryReader reader)
        {
            return new WarSurrenderPacket(reader.ReadString());
        }

        public static void Encode(WarSurrenderPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;

                IWarManager warManager = server.PlayerList.WarManager;
                IWar war = warManager.WarDict[warName];
                IWarParty warPartyAllies = war.GetWarPartyOfPlayer(player);

                warPartyAllies.PlayerSurrender(player);

                IWarParty warPartyEnemies = war.GetWarEnemyPartyOfPlayer(player);

                foreach(IPlayer playerInfo in warPartyAllies.Armies.Keys)
                {
                    ServerMessagePacket surrenderPacket = new ServerMessagePacket(player.PlayerName + "surrenders in war" + warName);
                    playerInfo.SendPacket(surrenderPacket);
                }

                foreach (IPlayer playerInfo in warPartyEnemies.Armies.Keys)
                {
                    ServerMessagePacket surrenderPacket = new ServerMessagePacket(player.PlayerName + "surrenders in war" + warName);
                    playerInfo.SendPacket(surrenderPacket);
                }
            }
        }
    }
}
