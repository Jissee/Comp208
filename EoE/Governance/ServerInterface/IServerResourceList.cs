using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    public interface IServerResourceList : IResourceList
    {
        void ClearAll();
    }
}
