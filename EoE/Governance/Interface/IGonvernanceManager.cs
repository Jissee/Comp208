namespace EoE.Governance.Interface
{
    /// <summary>
    /// The base interface of governance manager, including explorations.
    /// </summary>
    public interface IGonvernanceManager
    {
        static readonly int EXPLORE_RESOURCE_PER_POP = 5;
        void SetExploration(int inutPopulation);
        void SyntheticArmy(GameResourceType type, int count);
    }

}
