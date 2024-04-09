using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IServerGonveranceManager: IGonveranceManager
    {
        public IServerPopManager PopManager { get; }
        void SetExploration(int inutPopulation);
    }
}
