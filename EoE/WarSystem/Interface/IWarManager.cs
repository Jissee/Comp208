using EoE.Network.Entities;
using EoE.Server.WarSystem;
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
        Dictionary<string, IWar> PreparingWarDict { get; }
        Dictionary<IPlayer, Dictionary<IPlayer, WarTarget>> WarTargets { get; }
        IServer Server { get; }
        void PrepareNewWar(string name);
        void DeclareWar(string warName);
        void RemoveWar(IWar war);
        void PlayerLose(IPlayer player);
    }
}
