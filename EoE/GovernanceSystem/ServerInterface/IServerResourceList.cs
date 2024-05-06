using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerResourceList : IResourceList
    {
        void ClearAll();
    }
}
