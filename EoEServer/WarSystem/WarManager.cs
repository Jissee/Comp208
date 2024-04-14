using EoE.Network.Entities;
using EoE.WarSystem.Interface;

namespace EoE.Server.WarSystem
{
    public class WarManager : IWarManager, ITickable
    {
        public Dictionary<string, IWar> WarDict { get; private set; } = new Dictionary<string, IWar>();
        private List<string> removal = new List<string>();
        public IServer Server { get; }
        public WarManager(IServer server) 
        {
            this.Server = server;
        }
        public void DeclareWar(IWar war)
        {
            WarDict.Add(war.WarName, war);
            war.SetWarManager(this);
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
        }
    }
}
