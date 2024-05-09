using EoE.Governance.Interface;

namespace EoE.Governance.ClientInterface
{
    /// <summary>
    /// The client side interface of governance manager.
    /// </summary>
    public interface IClientGonveranceManager : IGonvernanceManager
    {
        IClientPopManager PopManager { get; }
        IClientFieldList FieldList { get; }
        IClientResourceList ResourceList { get; }
    }
}
