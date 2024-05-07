using EoE.Treaty;

namespace EoE.Server.Treaty
{
    public class PlayerRelation : IPlayerRelation
    {
        public ITreatyManager TreatyManager;
        public Dictionary<IPlayer, List<IPlayer>> ProtectedBy { get; set; }
        private List<IPlayer> AlreadyIn;
        public PlayerRelation(ITreatyManager treatyManager)
        {
            TreatyManager = treatyManager;
            ProtectedBy = new Dictionary<IPlayer, List<IPlayer>>();
            AlreadyIn = new List<IPlayer>();
        }
        public void AddProtector(IPlayer target, IPlayer protector)
        {
            if (!ProtectedBy.ContainsKey(target))
            {
                ProtectedBy[target] = [protector];
            }
            else
            {
                if (!ProtectedBy[target].Contains(protector))
                {
                    ProtectedBy[target].Add(protector);
                }
            }
        }
        public void RemoveProtector(IPlayer target, IPlayer protector)
        {
            if (!ProtectedBy.ContainsKey(target))
            {
                return;
            }
            if (ProtectedBy[target].Contains(protector))
            {
                ProtectedBy[target].Remove(protector);
            }
        }
        public List<IPlayer>? GetDirectProtectors(IPlayer target)
        {
            return ProtectedBy[target];
        }
        private void DeepSearch(IPlayer target)
        {
            foreach (IPlayer protector in ProtectedBy[target])
            {
                if (AlreadyIn.Contains(protector))
                {
                    continue;
                }
                else
                {
                    AlreadyIn.Add(protector);
                    DeepSearch(protector);
                }
            }
        }
        public List<IPlayer> GetProtectorsRecursively(IPlayer target)
        {
            UpdateProtectGraph();
            AlreadyIn = new List<IPlayer>();
            DeepSearch(target);
            if (AlreadyIn.Contains(target))
            {
                AlreadyIn.Remove(target);
            }
            return AlreadyIn;
        }
        private void UpdateProtectGraph()
        {
            ProtectedBy.Clear();
            TreatyManager.BuildPlayerProtected();
            foreach (var treaty in TreatyManager.RelationTreatyList)
            {
                if (treaty is ProtectiveTreaty)
                {
                    AddProtector(treaty.FirstParty, treaty.SecondParty);
                }
                else if (treaty is CommonDefenseTreaty)
                {
                    AddProtector(treaty.FirstParty, treaty.SecondParty);
                    AddProtector(treaty.SecondParty, treaty.FirstParty);
                }
            }
        }
    }
}
