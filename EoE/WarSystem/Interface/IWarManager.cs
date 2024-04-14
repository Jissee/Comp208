using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IWarManager : ITickable
    {
        Dictionary<string, IWar> WarDict { get; }
        void DeclareWar(IWar war);
        void RemoveWar(IWar war);
    }
}
