using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerResourceList : IServerResourceList
    {
        private Dictionary<GameResourceType, int> resources = new Dictionary<GameResourceType, int>();
        public ServerPlayerResourceList(
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
        public ServerPlayerResourceList() : this(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {

        }

        public void AddResource(GameResourceType type, int count)
        {
            AddResourceStack(new ResourceStack(type, count));
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
        public void ClearAll()
        {
            foreach (GameResourceType type in Enum.GetValues(typeof(GameResourceType)))
            {
                resources[type] = 0;
            }
        }
    }
}
