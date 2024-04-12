using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ClientInterface
{
    public interface IClientGonveranceManager : IGonveranceManager
    {
        IClientPopulationManager PopManager { get; }
        IClientFieldList FieldList { get; }
        IClientResourceList ResourceList { get; }
    }
}
