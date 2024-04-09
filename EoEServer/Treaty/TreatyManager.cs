﻿using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class TreatyManager : ITreatyManager, ITickable
    {
        public List<RelationTreaty> RelationTreatyList { get; init; }
        public List<TruceTreaty> TruceTreatyList { get; init; }
        public PlayerRelation PlayerRelation { get; init; }
        private ServerPlayerList serverPlayerList;
        public TreatyManager(ServerPlayerList serverPlayerList) 
        {
            RelationTreatyList = new List<RelationTreaty>();
            TruceTreatyList = new List<TruceTreaty>();
            PlayerRelation = new PlayerRelation(this);
            this.serverPlayerList = serverPlayerList;
        }
        public void AddRelationTreaty(RelationTreaty treaty)
        {
            if (treaty is CommonDefenseTreaty)
            {
                for (int i = 0; i < RelationTreatyList.Count; i++)
                {
                    var findTreaty = RelationTreatyList[i];
                    if ((findTreaty.FirstParty == treaty.FirstParty && findTreaty.SecondParty == treaty.SecondParty) ||
                        (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty))
                    {
                        RemoveRelationTreaty(findTreaty);
                        i--;
                    }
                }
            }
            if (treaty is ProtectiveTreaty)
            {
                for (int i = 0; i < RelationTreatyList.Count; i++)
                {
                    var findTreaty = RelationTreatyList[i];
                    if (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty && findTreaty is ProtectiveTreaty)
                    {
                        RemoveRelationTreaty(findTreaty);
                        i--;
                        CommonDefenseTreaty newDefenseTreaty = new CommonDefenseTreaty(findTreaty.FirstParty, findTreaty.SecondParty);
                        AddRelationTreaty(newDefenseTreaty);
                        return;
                    }
                }
            }
            if (!RelationTreatyList.Contains(treaty))
            {
                RelationTreatyList.Add(treaty);
            }
        }
        public void RemoveRelationTreaty(RelationTreaty treaty)
        {
            if (RelationTreatyList.Contains(treaty))
            {
                RelationTreatyList.Remove(treaty);
            }
        }
        public void AddTruceTreaty(TruceTreaty truceTreaty)
        {
            TruceTreatyList.Add(truceTreaty);
        }
        public void UpdateTruceTreaty()
        {
            for(int i = 0; i < TruceTreatyList.Count; i++)
            {
                var treaty = TruceTreatyList[i];
                treaty.Tick();
                if (!treaty.IsAvailable())
                {
                    TruceTreatyList.Remove(treaty);
                    i--;
                }
            }
        }
        public void UpdateRelationTreaty()
        {
            for (int i = 0; i < RelationTreatyList.Count; i++)
            {
                var treaty = RelationTreatyList[i];
                if(treaty is ProtectiveTreaty protective)
                {
                    if (!protective.IsAvailable())
                    {
                        RelationTreatyList.Remove(protective);
                        i--;
                        continue;
                    }
                }
                treaty.Tick();
            }
        }
        public List<IPlayer> FindNonTruce(IPlayer player)
        {
            List<IPlayer> remainPlayer = [.. serverPlayerList.Players];
            foreach(var truceTreaty in TruceTreatyList)
            {
                if(truceTreaty.FirstParty == player)
                {
                    remainPlayer.Remove(truceTreaty.SecondParty);
                }
                if(truceTreaty.SecondParty == player)
                {
                    remainPlayer.Remove(truceTreaty.FirstParty);
                }
            }
            return remainPlayer;
        }
        public void Tick()
        {
            UpdateRelationTreaty();
            UpdateTruceTreaty();
        }
    }
}
