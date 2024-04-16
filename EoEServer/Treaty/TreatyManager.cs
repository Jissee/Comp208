using EoE.GovernanceSystem;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class TreatyManager : ITreatyManager
    {
        public List<ITreaty> RelationTreatyList { get; init; }
        public List<ITreaty> TruceTreatyList { get; init; }
        public IPlayerRelation PlayerRelation { get; init; }
        private IServerPlayerList serverPlayerList;
        public TreatyManager(IServerPlayerList serverPlayerList) 
        {
            RelationTreatyList = new List<ITreaty>();
            TruceTreatyList = new List<ITreaty>();
            PlayerRelation = new PlayerRelation(this);
            this.serverPlayerList = serverPlayerList;
        }
        public void AddRelationTreaty(RelationTreaty treaty)
        {
            if (treaty is CommonDefenseTreaty)
            {
                for (int i = 0; i < RelationTreatyList.Count; i++)
                {
                    var findTreaty = (RelationTreaty)RelationTreatyList[i];
                    if ((findTreaty.FirstParty == treaty.FirstParty && findTreaty.SecondParty == treaty.SecondParty) ||
                        (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty) &&
                        findTreaty is ProtectiveTreaty)
                    {

                        RemoveRelationTreaty(findTreaty);
                        i--;
                    }
                    if (((findTreaty.FirstParty == treaty.FirstParty && findTreaty.SecondParty == treaty.SecondParty) ||
                        (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty)) &&
                        findTreaty is CommonDefenseTreaty)
                    {
                        ServerMessagePacket packet = new ServerMessagePacket("You have already signed a common defense treaty!");
                        return;
                    }
                }
            }
            if (treaty is ProtectiveTreaty)
            {
                for (int i = 0; i < RelationTreatyList.Count; i++)
                {
                    var findTreaty = (RelationTreaty)RelationTreatyList[i];
                    if (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty && findTreaty is ProtectiveTreaty)
                    {
                        RemoveRelationTreaty(findTreaty);
                        i--;
                        CommonDefenseTreaty newDefenseTreaty = new CommonDefenseTreaty(findTreaty.FirstParty, findTreaty.SecondParty);
                        AddRelationTreaty(newDefenseTreaty);
                        return;
                    }
                    if(((findTreaty.FirstParty == treaty.FirstParty && findTreaty.SecondParty == treaty.SecondParty) ||
                        (findTreaty.SecondParty == treaty.FirstParty && findTreaty.FirstParty == treaty.SecondParty)) &&
                        findTreaty is CommonDefenseTreaty)
                    {
                        ServerMessagePacket packet = new ServerMessagePacket("You have already signed a common defense treaty!");
                        return;
                    }
                    if ((findTreaty.FirstParty == treaty.FirstParty && findTreaty.SecondParty == treaty.SecondParty) && findTreaty is ProtectiveTreaty)
                    {
                        ServerMessagePacket packet = new ServerMessagePacket("You have already signed a protective defense treaty!");
                        return;
                    }
                }
            }
            if (!RelationTreatyList.Contains(treaty))
            {
                RelationTreatyList.Add(treaty);
            }
        }
        public void RemoveRelationTreaty(ITreaty treaty)
        {
            if (RelationTreatyList.Contains(treaty))
            {
                RelationTreatyList.Remove(treaty);
            }
        }
        public void AddTruceTreaty(ITreaty truceTreaty)
        {
            TruceTreatyList.Add((TruceTreaty)truceTreaty);
        }
        public void AddTruceTreaty(IPlayer player1, IPlayer player2)
        {
            TruceTreaty truceTreaty = new TruceTreaty((ServerPlayer)player1, (ServerPlayer)player2, 10);
            AddTruceTreaty((TruceTreaty)truceTreaty);
        }
        public void UpdateTruceTreaty()
        {
            for(int i = 0; i < TruceTreatyList.Count; i++)
            {
                var treaty = (TruceTreaty)TruceTreatyList[i];
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
                        ServerMessagePacket packetF = new ServerMessagePacket($"You do not have enough resources to get protection from {treaty.SecondParty}");
                        ServerMessagePacket packetS = new ServerMessagePacket($"{treaty.FirstParty} cannot afford resources to get your protection");
                        treaty.FirstParty.SendPacket(packetF);
                        treaty.SecondParty.SendPacket(packetS);
                        continue;
                    }
                }

                if(treaty is ITickableTreaty tickable)
                {
                    tickable.Tick();
                }
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

        public void AddProtectiveTreaty(IPlayer target, IPlayer protector, ResourceListRecord condition)
        {
            ProtectiveTreaty treaty = new ProtectiveTreaty(target, protector);
            treaty.AddCondition(new ResourceStack(GameResourceType.Silicon,condition.siliconCount));
            treaty.AddCondition(new ResourceStack(GameResourceType.Copper, condition.copperCount));
            treaty.AddCondition(new ResourceStack(GameResourceType.Iron, condition.ironCount));
            treaty.AddCondition(new ResourceStack(GameResourceType.Aluminum, condition.aluminumCount));
            treaty.AddCondition(new ResourceStack(GameResourceType.Electronic, condition.electronicCount));
            treaty.AddCondition(new ResourceStack(GameResourceType.Industrial, condition.industrialCount));
            AddRelationTreaty(treaty);
        }

        public void AddCommonDefenseTreaty(IPlayer player1, IPlayer player2)
        {
            AddRelationTreaty(new CommonDefenseTreaty(player1, player2));
        }

    }
}
