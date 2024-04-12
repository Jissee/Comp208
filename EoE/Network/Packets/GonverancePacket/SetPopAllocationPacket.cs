using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.GonverancePacket
{
    public class SetPopAllocationPacket : IPacket<SetPopAllocationPacket>
    {
        private int siliconPop;
        private int copperPop;
        private int ironPop;
        private int aluminumPop;
        private int electronicPop;
        private int industrailPop;

        public SetPopAllocationPacket(
            int siliconPop,
            int copperPop,
            int ironPop,
            int aluminumPop,
            int electronicPop,
            int industrailPop
            )
        {
            this.siliconPop = siliconPop;
            this.copperPop = copperPop;
            this.ironPop = ironPop;
            this.aluminumPop = aluminumPop;
            this.electronicPop = electronicPop;
            this.industrailPop = industrailPop;
        }
        public static SetPopAllocationPacket Decode(BinaryReader reader)
        {
            return new SetPopAllocationPacket(
                reader.ReadInt32(), 
                reader.ReadInt32(), 
                reader.ReadInt32(),
                reader.ReadInt32(), 
                reader.ReadInt32(),
                reader.ReadInt32()
                );           
        }

        public static void Encode(SetPopAllocationPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.siliconPop);
            writer.Write(obj.copperPop);
            writer.Write(obj.ironPop);
            writer.Write(obj.aluminumPop);
            writer.Write(obj.industrailPop);
            writer.Write(obj.electronicPop);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IServer server)
                {
                    IPlayer player = context.PlayerSender;
                    IServerPopManager populationManager = player.GonveranceManager.PopManager;
                    List<int> list = [siliconPop, copperPop, ironPop, aluminumPop, electronicPop, industrailPop];
                    if (list.Min() < 0)
                    {
                        //TODO send packet
                    }
                    int count = siliconPop + copperPop + ironPop + aluminumPop + industrailPop + electronicPop;
                    if (count > populationManager.TotalPopulation)
                    {
                        throw new Exception("");
                        ///Todo
                    }
                    else
                    {
                        populationManager.ResetPopAllocation();
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, siliconPop);
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, copperPop);
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, ironPop);
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, aluminumPop);
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, industrailPop);
                        populationManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, electronicPop);
                    }
                }
            }
        }
    }
}
