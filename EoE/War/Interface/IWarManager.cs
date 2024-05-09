using EoE.Network.Entities;

namespace EoE.War.Interface
{
    /// <summary>
    /// Managing all wars including declaring wars and surrender.
    /// </summary>
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
