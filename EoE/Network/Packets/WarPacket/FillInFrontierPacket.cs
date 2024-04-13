using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.GovernanceSystem.ServerInterface;
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
    public class FillInFrontierPacket : IPacket<FillInFrontierPacket>
    {
        private string warName;
        private int fillBattle;
        private int fillInformative;
        private int fillMechanism;
        public FillInFrontierPacket(string warName, int fillBattle, int fillInformative, int fillMechanism)
        {
            this.warName = warName;
            this.fillBattle = fillBattle;
            this.fillInformative = fillInformative;
            this.fillMechanism = fillMechanism;
        }

        public static FillInFrontierPacket Decode(BinaryReader reader)
        {
            return new FillInFrontierPacket(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        public static void Encode(FillInFrontierPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
            writer.Write(obj.fillBattle);
            writer.Write(obj.fillInformative);
            writer.Write(obj.fillMechanism);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;

                IWarManager warManager = server.PlayerList.WarManager;
                IWar war = warManager.WarDict[warName];
                IWarParty warParty = war.GetWarPartyOfPlayer(player);
                IServerResourceList resourceList = player.GonveranceManager.ResourceList;
                if(fillBattle + fillInformative + fillMechanism < warParty.WarWidth)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("Your soldiers are insufficently assigned. You need to reposition them; otherwise, you will automatically surrender.");
                    player.SendPacket(packet);
                    player.SendPacket(new ResourceUpdatePacket(resourceList.GetResourceListRecord()));
                    return;
                }
                
                if(resourceList.GetResourceCount(GameResourceType.BattleArmy) < fillBattle)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("You don't have enough battle army!");
                    player.SendPacket(packet);
                    player.SendPacket(new ResourceUpdatePacket(resourceList.GetResourceListRecord()));
                    return;
                }
                if (resourceList.GetResourceCount(GameResourceType.InformativeArmy) < fillInformative)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("You don't have enough informative army!");
                    player.SendPacket(packet);
                    player.SendPacket(new ResourceUpdatePacket(resourceList.GetResourceListRecord()));
                    return;
                }
                if (resourceList.GetResourceCount(GameResourceType.MechanismArmy) < fillMechanism)
                {
                    ServerMessagePacket packet = new ServerMessagePacket("You don't have enough mechanism army!");
                    player.SendPacket(packet);
                    player.SendPacket(new ResourceUpdatePacket(resourceList.GetResourceListRecord()));
                    return;
                }
                if (warParty.HasFilled(player))
                {
                    ServerMessagePacket packet = new ServerMessagePacket("You have already fill in the frontier!");
                    player.SendPacket(packet);
                    player.SendPacket(new ResourceUpdatePacket(resourceList.GetResourceListRecord()));
                    return;
                }
                resourceList.SplitResource(GameResourceType.BattleArmy, fillBattle);
                resourceList.SplitResource(GameResourceType.InformativeArmy, fillInformative);
                resourceList.SplitResource(GameResourceType.MechanismArmy, fillMechanism);

                warParty.FillInFrontier(player, fillBattle, fillInformative, fillMechanism);
            }
        }
    }
}
