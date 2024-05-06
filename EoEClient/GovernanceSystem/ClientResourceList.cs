using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Client.GovernanceSystem
{
    public class ClientResourceList : IClientResourceList
    {
        private Dictionary<GameResourceType, int> resources = new Dictionary<GameResourceType, int>();
        public ClientResourceList(
            int silicon,
            int copper,
            int iron,
            int aluminum,
            int electronic,
            int industrial,
            int battleArmy,
            int informativeArmy,
            int mechanismArmy)
        {
            resources.Add(GameResourceType.Silicon, silicon);
            resources.Add(GameResourceType.Copper, copper);
            resources.Add(GameResourceType.Iron, iron);
            resources.Add(GameResourceType.Aluminum, aluminum);
            resources.Add(GameResourceType.Electronic, electronic);
            resources.Add(GameResourceType.Industrial, industrial);
            resources.Add(GameResourceType.BattleArmy, battleArmy);
            resources.Add(GameResourceType.InformativeArmy, informativeArmy);
            resources.Add(GameResourceType.MechanismArmy, mechanismArmy);
        }
        public ClientResourceList() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public void AddResource(GameResourceType type, int count)
        {
            resources[type] += count;
        }
        public void AddResourceStack(ResourceStack adder)
        {
            resources[adder.Type] += adder.Count;
        }
        public ResourceStack SplitResource(GameResourceType type, int count)
        {
            int count1 = resources[type];
            if (count1 >= count)
            {
                resources[type] -= count;
                return new ResourceStack(type, count);
            }
            else
            {
                resources[type] = 0;
                return new ResourceStack(type, count1);
            }
        }
        public ResourceStack SplitResourceStack(ResourceStack stack)
        {
            return SplitResource(stack.Type, stack.Count);
        }
        public int GetResourceCount(GameResourceType type)
        {
            return resources[type];
        }

        public ResourceStack GetReourceStack(GameResourceType type)
        {
            return new ResourceStack(type, resources[type]);
        }

        public void Synchronize(ResourceListRecord resourceListRecord)
        {
            resources[GameResourceType.Silicon] = resourceListRecord.siliconCount;
            resources[GameResourceType.Copper] = resourceListRecord.copperCount;
            resources[GameResourceType.Iron] = resourceListRecord.ironCount;
            resources[GameResourceType.Aluminum] = resourceListRecord.aluminumCount;
            resources[GameResourceType.Electronic] = resourceListRecord.electronicCount;
            resources[GameResourceType.Industrial] = resourceListRecord.industrialCount;

            resources[GameResourceType.BattleArmy] = resourceListRecord.battleArmyCount;
            resources[GameResourceType.InformativeArmy] = resourceListRecord.informativeArmyCount;
            resources[GameResourceType.MechanismArmy] = resourceListRecord.mechanismArmyCount;
            WindowManager.INSTANCE.UpdateResources();
        }

        public ResourceListRecord GetResourceListRecord()
        {
            return new ResourceListRecord(
                resources[GameResourceType.Silicon],
                resources[GameResourceType.Copper],
                resources[GameResourceType.Iron],
                resources[GameResourceType.Aluminum],
                resources[GameResourceType.Electronic],
                resources[GameResourceType.Industrial],
                resources[GameResourceType.BattleArmy],
                resources[GameResourceType.InformativeArmy],
                resources[GameResourceType.MechanismArmy]
                );
        }
    }
}
