using EoE.Network.Entities;
using EoE.WarSystem.Interface;

namespace EoE.Server.WarSystem
{
    public class WarManager : IWarManager, ITickable
    {
        public Dictionary<string, IWar> WarDict { get; private set; } = new Dictionary<string, IWar>();
        public Dictionary<string, IWar> PreparingWarDict { get; private set; } = new Dictionary<string, IWar>();
        public Dictionary<IPlayer, Dictionary<IPlayer, WarTarget>> WarTargets { get; private set; }

        private List<string> removal = new List<string>();
        public IServer Server { get; }
        public WarManager(IServer server) 
        {
            this.Server = server;
            this.WarTargets = new Dictionary<IPlayer, Dictionary<IPlayer, WarTarget>>();
        }
        public void PrepareNewWar(string name)
        {
            if (PreparingWarDict.ContainsKey(name))
            {
                PreparingWarDict.Remove(name);
            }
            IWarParty attackers = new WarParty();
            IWarParty defenders = new WarParty();
            IWar newWar = new War(attackers, defenders, name);
            PreparingWarDict.Add(name, newWar);
        }
        public void DeclareWar(string warName)
        {
            IWar war = PreparingWarDict[warName];
            if (war != null)
            {
                WarDict.Add(war.WarName, war);
                PreparingWarDict.Remove(warName);
                war.SetWarManager(this);
            }
        }
        public void RemoveWar(IWar war)
        {
            if (WarDict.ContainsValue(war))
            {
                removal.Add(war.WarName);
            }
        }
        public void PlayerLose(IPlayer player)
        {
            foreach (var kvp in WarDict)
            {
                IWar war = kvp.Value;
                IWarParty attackers = war.Attackers;
                IWarParty defenders = war.Defenders;
                if (attackers.Contains(player))
                {
                    attackers.PlayerLose(player);
                }
                if (defenders.Contains(player))
                {
                    defenders.PlayerLose(player);
                }
            }
        }
        public void Tick()
        {
            foreach(var kvp in WarDict)
            {
                var war = kvp.Value;
                war.Tick();
            }
            if(removal.Count > 0)
            {
                foreach(string warname in removal)
                {
                    WarDict.Remove(warname);
                }
                removal.Clear();
            }
            PreparingWarDict.Clear();
        }
    }
}
