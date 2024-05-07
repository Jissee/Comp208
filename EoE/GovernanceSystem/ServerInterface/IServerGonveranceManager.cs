using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerGonveranceManager : IGonveranceManager, ITickable
    {
        bool IsLose { get; }
        PlayerStatus PlayerStatus { get; }
        IServerPopManager PopManager { get; }
        IServerFieldList FieldList { get; }
        IServerResourceList ResourceList { get; }
        void SyntheticArmy(GameResourceType type, int count);
        void ClearAll();
    }
}
