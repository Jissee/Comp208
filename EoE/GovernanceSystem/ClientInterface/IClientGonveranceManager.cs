using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientGonveranceManager : IGonvernanceManager
    {
        IClientPopManager PopManager { get; }
        IClientFieldList FieldList { get; }
        IClientResourceList ResourceList { get; }
        void SyntheticArmy(GameResourceType type, int count);
    }
}
