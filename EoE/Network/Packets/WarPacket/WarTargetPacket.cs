using EoE.Network.Entities;
using EoE.Server.WarSystem;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarTargetPacket : IPacket<WarTargetPacket>
    {
        private string targetName;
        private WarTarget target;
        public WarTargetPacket(string targetName, WarTarget target) 
        { 
            this.targetName = targetName;
            this.target = target;
        }

        public static WarTargetPacket Decode(BinaryReader reader)
        {
            return new WarTargetPacket(reader.ReadString(), WarTarget.decoder(reader));
        }

        public static void Encode(WarTargetPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.targetName);
            WarTarget.encoder(obj.target, writer);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                IPlayer targetPlayer = server.GetPlayer(targetName)!;
                if (!server.PlayerList.WarManager.WarTargets.ContainsKey(player))
                {
                    Dictionary<IPlayer, WarTarget> playerTarget = new Dictionary<IPlayer, WarTarget>();
                    playerTarget.Add(targetPlayer, target);
                    server.PlayerList.WarManager.WarTargets.Add(player, playerTarget);
                }
                else 
                {
                    if (!server.PlayerList.WarManager.WarTargets[player].ContainsKey(targetPlayer))
                    {
                        server.PlayerList.WarManager.WarTargets[player].Add(targetPlayer, target);
                    }
                    else
                    {
                        server.PlayerList.WarManager.WarTargets[player][targetPlayer] = target;
                    }
                }
            }
        }
    }
}
