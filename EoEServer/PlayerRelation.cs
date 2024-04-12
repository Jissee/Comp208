using EoE.Server.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class PlayerRelation: IPlayerRelation
    {
        public TreatyManager TreatyManager;
        public Dictionary<ServerPlayer, List<ServerPlayer>> ProtectedBy;
        private List<ServerPlayer> AlreadyIn;
        public PlayerRelation(TreatyManager treatyManager)
        {
            TreatyManager = treatyManager;
            ProtectedBy = new Dictionary<ServerPlayer, List<ServerPlayer>>();
            AlreadyIn = new List<ServerPlayer>();
        }
        public void AddProtector(ServerPlayer target, ServerPlayer protector)
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
        public void RemoveProtector(ServerPlayer target, ServerPlayer protector)
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
        public List<ServerPlayer>? GetDirectProtectors(ServerPlayer target)
        {
            return ProtectedBy[target];
        }
        private void DeepSearch(ServerPlayer target)
        {
            foreach(ServerPlayer protector in ProtectedBy[target])
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
        public List<ServerPlayer> GetProtectorsRecursively(ServerPlayer target)
        {
            UpdateProtectGraph();
            AlreadyIn = new List<ServerPlayer>();
            DeepSearch(target);
            if(AlreadyIn.Contains(target))
            {
                AlreadyIn.Remove(target);
            }
            return AlreadyIn;
        }
        private void UpdateProtectGraph()
        {
            ProtectedBy.Clear();
            foreach(var treaty in TreatyManager.RelationTreatyList)
            {
                if(treaty is ProtectiveTreaty)
                {
                    AddProtector(treaty.FirstParty, treaty.SecondParty);
                }
                else if(treaty is CommonDefenseTreaty)
                {
                    AddProtector(treaty.FirstParty, treaty.SecondParty);
                    AddProtector(treaty.SecondParty, treaty.FirstParty);
                }
            }
        }
    }
}
