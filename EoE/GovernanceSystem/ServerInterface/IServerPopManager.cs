using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerPopManager : IPopManager
    {
        int ExploratoinPopulation { get; }
        void AlterPop(int count);
        void SetExploration(int population);
        void ClearAll();
    }
}
