using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    /// <summary>
    /// The server side interface of resource list.
    /// </summary>
    public interface IServerResourceList : IResourceList
    {
        void ClearAll();
    }
}
