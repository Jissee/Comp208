using EoE.Network.Entities;
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
        IServer Server { get; }
        void DeclareWar(IWar war);
        void RemoveWar(IWar war);
        void PlayerLose(IPlayer player);
    }
}
