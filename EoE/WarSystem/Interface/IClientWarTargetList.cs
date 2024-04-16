using EoE.Server.WarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IClientWarTargetList
    {
        void ChangeClaim(string name, WarTarget warTarget);
    }
}
