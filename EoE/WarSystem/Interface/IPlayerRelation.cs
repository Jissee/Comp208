using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IPlayerRelation
    {
        Dictionary<IPlayer, List<IPlayer>> ProtectedBy { get; set; }
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
    }
}
