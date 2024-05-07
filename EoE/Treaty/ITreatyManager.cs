﻿using EoE.Network.Packets.GonverancePacket.Record;

namespace EoE.Treaty
{
    public interface ITreatyManager : ITickable
    {
        List<ITreaty> RelationTreatyList { get; }
        IPlayerRelation PlayerRelation { get; }
        void BuildPlayerProtected();
        List<IPlayer> FindNonTruce(IPlayer player);
        void AddProtectiveTreaty(IPlayer target, IPlayer protector, ResourceListRecord condition);
        void AddCommonDefenseTreaty(IPlayer player1, IPlayer player2);
        void AddTruceTreaty(IPlayer player1, IPlayer player2);
        void RemoveRelationTreaty(ITreaty treaty);
        void ClearAll(IPlayer player);

    }
}
