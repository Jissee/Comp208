using EoE.Governance;
using EoE.Governance.ClientInterface;
using EoE.Network.Packets.GonverancePacket;
using System.Windows;
using static EoE.Governance.Interface.IGonvernanceManager;

namespace EoE.Client.Governance
{
    public class ClientGovernance : IClientGonveranceManager
    {
        public IClientFieldList FieldList { get; init; }
        public IClientResourceList ResourceList { get; init; }
        public IClientPopManager PopManager { get; init; }
        public ClientGovernance()
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
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type, count));
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
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type, count));
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
                        Client.INSTANCE.SendPacket(new SyntheticArmyPacket(type, count));
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
