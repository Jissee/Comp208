namespace EoE.GovernanceSystem.Interface
{
    public interface IGonvernanceManager
    {
        static readonly int EXPLORE_RESOURCE_PER_POP = 5;
        void SetExploration(int inutPopulation);

        void SyntheticArmy(GameResourceType type, int count);
    }

}
