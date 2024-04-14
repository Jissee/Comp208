using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Treaty
{
    public interface ITreatyManager :ITickable
    {
        List<ITreaty> RelationTreatyList { get;}
        IPlayerRelation PlayerRelation { get; }
        List<IPlayer> FindNonTruce(IPlayer player);
    }
}
