using EoE.GovernanceSystem;
using EoE.Treaty;

namespace EoE.Server.Treaty
{
    public class CompensationTreaty : RelationTreaty, ITickableTreaty
    {
        private int remainingTime;
        public CompensationTreaty(IPlayer firstParty, IPlayer secondParty, int remainingTime) : base(firstParty, secondParty)
        {
            this.remainingTime = remainingTime;
        }
        public bool IsAvailable()
        {
            return remainingTime > 0;
        }

        public void ConsumeRecourse()
        {
            foreach (var kvp in ConditionEntries)
            {
                int count = FirstParty.GonveranceManager.ResourceList.GetResourceCount(kvp.Key);
                int maxCount = Math.Max(count, kvp.Value);
                ResourceStack addStack = FirstParty.GonveranceManager.ResourceList.SplitResource(kvp.Key, maxCount);
                SecondParty.GonveranceManager.ResourceList.AddResourceStack(addStack);
            }
        }

        public void Tick()
        {
            ConsumeRecourse();
            remainingTime--;
        }
    }
}
