using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Governance.Interface
{
    /// <summary>
    /// The base interface of resource list, including add or remove resources.
    /// </summary>
    public interface IResourceList
    {
        int GetResourceCount(GameResourceType type);
        ResourceStack GetReourceStack(GameResourceType type);
        ResourceStack SplitResourceStack(ResourceStack stack);
        ResourceStack SplitResource(GameResourceType type, int count);
        void AddResourceStack(ResourceStack adder);
        void AddResource(GameResourceType type, int count);
        ResourceListRecord GetResourceListRecord();
    }
}
