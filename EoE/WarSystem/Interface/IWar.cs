using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IWar : ITickable
    {
        public string WarName { get; }
        public IWarParty Attackers { get; }
        public IWarParty Defenders { get; }
        public IWarTarget AttackersTarget { get; }
        public IWarTarget DefendersTarget { get; }
        void SetWarManager(IWarManager manager);
        IWarParty GetWarPartyOfPlayer(IPlayer player);
        void End(IWarParty defeated);
    }
}
