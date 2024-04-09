using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IClietGonveranceManager: IGonveranceManager
    {
        public IPopulationManager PopManager { get; }
    }
}
