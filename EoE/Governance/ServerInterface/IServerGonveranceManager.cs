using EoE.Governance.Interface;

namespace EoE.Governance.ServerInterface
{
    public interface IServerGonveranceManager : IGonvernanceManager, ITickable
    {
        bool IsLose { get; }
        PlayerStatus PlayerStatus { get; }
        IServerPopManager PopManager { get; }
        IServerFieldList FieldList { get; }
        IServerResourceList ResourceList { get; }
        void ClearAll();
    }
}
