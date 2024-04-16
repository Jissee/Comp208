using EoE.Network.Entities;
using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarQueryTarget : IPacket<WarQueryTarget>
    {
        string targetName;
        private WarTarget target;
        public WarQueryTarget(string targetName, WarTarget target) 
        {
            this.targetName = targetName;
            this.target = target;
        }
        public static WarQueryTarget Decode(BinaryReader reader)
        {
            return new WarQueryTarget(reader.ReadString(), WarTarget.decoder(reader));
        }

        public static void Encode(WarQueryTarget obj, BinaryWriter writer)
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
                        WarQueryTarget packet = new WarQueryTarget(targetName, server.PlayerList.WarManager.WarTargets[player][targetPlayer]);
                        player.SendPacket(packet);
                    }
                    else
                    {
                        WarTarget tempTarget = new WarTarget();
                        WarQueryTarget packet = new WarQueryTarget(targetName, tempTarget);
                        player.SendPacket(packet);
                    }
                }
                else
                {

                    WarTarget tempTarget = new WarTarget();
                    WarQueryTarget packet = new WarQueryTarget(targetName, tempTarget);
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
