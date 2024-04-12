using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Treaty
{
    public interface ITreatyManager
    {
        List<IPlayer> FindNonTruce(IPlayer player);
    }
}
