using EoE.Network.Entities;

namespace EoE.War.Interface
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
