using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarManager : IWarManager, ITickable
    {
        public List<War> WarList = new List<War>();
        public WarManager() { }
        public void DeclareWar(War war)
        {
            WarList.Add(war);
        }
        public void EndWar(War war)
        {
            if (WarList.Contains(war))
            {
                war.End();
                WarList.Remove(war);
            }
        }
        public void PlayerLose(ServerPlayer player)
        {
            foreach (War war in WarList)
            {
                
            }
        }
        public void Tick()
        {
        }
    }
}
