using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    /// <summary>
    /// The server side interface of population manager
    /// </summary>
    public interface IServerPopManager : IPopManager
    {
        int ExploratoinPopulation { get; }
        void AlterPop(int count);
        void SetExploration(int population);
        void ClearAll();
    }
}
