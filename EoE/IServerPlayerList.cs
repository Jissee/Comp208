using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE
{
    public interface IServerPlayerList
    {
        ITreatyManager TreatyManager { get; }
        IWarManager WarManager { get; }

    }
}
