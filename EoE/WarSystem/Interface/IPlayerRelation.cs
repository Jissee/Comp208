using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IPlayerRelation
    {
        List<IPlayer> GetProtectorsRecursively(IPlayer target);
    }
}
