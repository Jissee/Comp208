using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GameEventPacket
{
    public class PlayerListPacket : IPacket<PlayerListPacket>
    {
        private List<string> playerList;
        private int length;

        public PlayerListPacket(List<string> playerList,int length)
        {
            this.playerList = playerList;
            this.length = length;
        }
        public static PlayerListPacket Decode(BinaryReader reader)
        {
            List<string> playerList = new List<string>();

            int length = reader.ReadInt32();
            for (int i = 0; i < length; i++)
            {
                playerList.Add(reader.ReadString());
            }

            return new PlayerListPacket(playerList,length);
        }

        public static void Encode(PlayerListPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.length);
            for (int i = 0; i < obj.length; i++)
            {
                writer.Write(obj.playerList[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    client.SynchronizeOtherPlayersName(playerList);
                }
            }
        }
    }
}
