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
    public class WarDeclarationPacket : IPacket<WarDeclarationPacket>
    {

        private string warName;
        public WarDeclarationPacket(string warName) 
        { 
            this.warName = warName;
        }
        public static WarDeclarationPacket Decode(BinaryReader reader)
        {
            return new WarDeclarationPacket(reader.ReadString());
        }

        public static void Encode(WarDeclarationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IWarManager manager = server.PlayerList.WarManager;
                manager.DeclareWar(warName);
                
                
                IWar war = server.PlayerList.WarManager.WarDict[warName];
                IWarParty attackers = war.Attackers;
                IWarParty defenders = war.Defenders;

                WarTarget attackersTarget = new WarTarget();
                WarTarget defendersTarget = new WarTarget();

                foreach (IPlayer attacker in attackers.Armies.Keys)
                {
                    foreach (IPlayer defender in defenders.Armies.Keys)
                    {
                        WarTarget target1 = manager.WarTargets[attacker][defender];
                        WarTarget target2 = manager.WarTargets[defender][attacker];
                        attackersTarget.Add(target1);
                        defendersTarget.Add(target2);
                    }
                }

                

            }
        }
    }
}
