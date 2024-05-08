using EoE.Governance.Interface;

namespace EoE.Governance.ClientInterface
{
    public interface IClientGonveranceManager : IGonvernanceManager
    {
        IClientPopManager PopManager { get; }
        IClientFieldList FieldList { get; }
        IClientResourceList ResourceList { get; }
    }
}
