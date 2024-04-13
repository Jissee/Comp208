using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ClientInterface;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EoE.GovernanceSystem.Interface.IGonveranceManager;

namespace EoE.Client.GovernanceSystem
{
    public class ClientGoverance: IClientGonveranceManager
    {
        public IClientFieldList FieldList { get; init; }
        public IClientResourceList ResourceList { get; init; }
        public IClientPopulationManager PopManager { get; init; }

        public ClientGoverance()
        {
            FieldList = new ClientFieldList();
            ResourceList = new ClientResourceList();
            PopManager = new ClientPopulationManager();
        }

        public void SetExploration(int inutPopulation)
        {
            if (inutPopulation < 0)
            {
                Client.INSTANCE.MsgBox("Negative input");
            }
            if (inutPopulation > PopManager.AvailablePopulation)
            {
                Client.INSTANCE.MsgBox("Insufficient available population");
            }
            else
            {
                int consume = inutPopulation * EXPLORE_RESOURCE_PER_POP;

                if (ResourceList.GetResourceCount(GameResourceType.Silicon) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Copper) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Iron) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Aluminum) >= consume)
                {
                    ResourceList.SplitResource(GameResourceType.Silicon, consume);
                    ResourceList.SplitResource(GameResourceType.Copper, consume);
                    ResourceList.SplitResource(GameResourceType.Iron, consume);
                    ResourceList.SplitResource(GameResourceType.Aluminum, consume);
                    Client.INSTANCE.SendPacket(new SetExplorationPacket(inutPopulation));
                }
                else
                {
                    Client.INSTANCE.MsgBox("Insufficient resources");
                }
            }
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
