using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    //first is protected by second
    public class ProtectiveTreaty : RelationTreaty
    {
        public ProtectiveTreaty(ServerPlayer firstParty, ServerPlayer secondParty) : base(firstParty, secondParty)
        {

        }
        public bool IsAvailable()
        {
            bool checkAvailable = true;
            foreach(var entry in ConditionEntries)
            {
                checkAvailable = checkAvailable && (FirstParty.GonveranceManager.ResourceList.GetResourceCount(entry.Key) >= ConditionEntries[entry.Key]);
            }
            return checkAvailable;
        }
        public void ConsumeRecourse()
        {
            foreach(var entry in ConditionEntries)
            {
                ResourceStack addStack = FirstParty.GonveranceManager.ResourceList.SplitResourceStack(entry.Key, entry.Value);
                SecondParty.GonveranceManager.ResourceList.AddResource(addStack);
            }
        }
        public override void Tick()
        {
            ConsumeRecourse();
        }
    }
}
