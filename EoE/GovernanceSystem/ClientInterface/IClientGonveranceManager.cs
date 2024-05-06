using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientGonveranceManager : IGonveranceManager
    {
        IClientPopulationManager PopManager { get; }
        IClientFieldList FieldList { get; }
        IClientResourceList ResourceList { get; }
        void SyntheticArmy(GameResourceType type, int count);
        void SetExploration(int inutPopulation);
    }
}
