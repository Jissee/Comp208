using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ClientInterface;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Server.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                    PopManager.AlterAvailablePop(-inutPopulation);
                    Client.INSTANCE.SendPacket(new SetExplorationPacket(inutPopulation));
                }
                else
                {
                    Client.INSTANCE.MsgBox("Insufficient resources");
                }
            }
        }

        public void SyntheticArmy(GameResourceType type, int count)
        {
            if ((int)type < (int)GameResourceType.BattleArmy)
            {
                MessageBox.Show("Please input an valid army type");
            }

            ResourceStack army = new ResourceStack(type, count);
            int popCount;
            ResourceStack resource;
            switch (type)
            {
                case GameResourceType.BattleArmy:
                    (popCount, resource) = Recipes.BattleArmyproduce(army);

                    if (popCount <= PopManager.AvailablePopulation)
                    {
                        PopManager.AlterAvailablePop(-popCount);
                        ResourceList.AddResourceStack(army);
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type, count));
                        WindowManager.INSTANCE.UpdateResources();
                        WindowManager.INSTANCE.UpdatePopulation();
                    }
                    else
                    {
                        MessageBox.Show("No enough available population");

                    }
                    break;
                case GameResourceType.InformativeArmy:
                    (popCount, resource) = Recipes.produceInfomativeArmy(army);
                    if (popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Electronic))
                    {
                        PopManager.AlterAvailablePop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type, count));
                        WindowManager.INSTANCE.UpdateResources();
                        WindowManager.INSTANCE.UpdatePopulation();
                    }
                    else
                    {
                        MessageBox.Show("No enough available population or resources");
                    }
                    break;
                case GameResourceType.MechanismArmy:
                    (popCount, resource) = Recipes.produceMechanismArmy(army);
                    if (popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Industrial))
                    {
                        PopManager.AlterAvailablePop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type,count));
                        WindowManager.INSTANCE.UpdateResources();
                        WindowManager.INSTANCE.UpdatePopulation();
                    }
                    else
                    {
                        MessageBox.Show("No enough available population or resources");
                    }
                    break;
                default:
                    throw new Exception("no such type");
            }

        }
    }
}
