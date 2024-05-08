using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    public interface IServerPopManager : IPopManager
    {
        int ExploratoinPopulation { get; }
        void AlterPop(int count);
        void SetExploration(int population);
        void ClearAll();
    }
}
