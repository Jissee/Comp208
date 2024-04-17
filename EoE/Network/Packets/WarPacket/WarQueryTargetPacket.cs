using EoE.Network.Entities;
using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarQueryTargetPacket : IPacket<WarQueryTargetPacket>
    {
        private string targetName;
        private WarTarget target;
        public WarQueryTargetPacket(string targetName, WarTarget target) 
        {
            this.targetName = targetName;
            this.target = target;
        }
        public static WarQueryTargetPacket Decode(BinaryReader reader)
        {
            return new WarQueryTargetPacket(reader.ReadString(), WarTarget.decoder(reader));
        }

        public static void Encode(WarQueryTargetPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.targetName);
            WarTarget.encoder(obj.target, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IPlayer player = context.PlayerSender!;
                IServer server = (IServer)context.Receiver;
                IPlayer targetPlayer = server.GetPlayer(targetName)!;
                if (server.PlayerList.WarManager.WarTargets.ContainsKey(player))
                {
                    if (server.PlayerList.WarManager.WarTargets[player].ContainsKey(targetPlayer))
                    {
                        WarQueryTargetPacket packet = new WarQueryTargetPacket(targetName, server.PlayerList.WarManager.WarTargets[player][targetPlayer]);
                        player.SendPacket(packet);
                    }
                    else
                    {
                        WarTarget tempTarget = new WarTarget();
                        WarQueryTargetPacket packet = new WarQueryTargetPacket(targetName, tempTarget);
                        player.SendPacket(packet);
                    }
                }
                else
                {

                    WarTarget tempTarget = new WarTarget();
                    WarQueryTargetPacket packet = new WarQueryTargetPacket(targetName, tempTarget);
                    player.SendPacket(packet);
                }
            }
            else
            {
                IClient client = (IClient) context.Receiver;
                client.ClientWarTargetList.ChangeClaim(targetName, target);
            }
        }
    }
}
