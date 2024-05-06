using EoE.GovernanceSystem;

namespace EoE.Server.Treaty
{
    public abstract class RelationTreaty : Treaty
    {
        public Dictionary<GameResourceType, int> ConditionEntries { get; init; }

        public RelationTreaty(IPlayer firstParty, IPlayer secondParty) : base(firstParty, secondParty)
        {
            ConditionEntries = new Dictionary<GameResourceType, int>();
        }
        public void AddCondition(ResourceStack resourceStack)
        {
            if (resourceStack.Count == 0)
            {
                return;
            }
            if (!ConditionEntries.ContainsKey(resourceStack.Type))
            {
                ConditionEntries[resourceStack.Type] = resourceStack.Count;
            }
            else
            {
                ConditionEntries[resourceStack.Type] += resourceStack.Count;
            }
        }
    }
}
