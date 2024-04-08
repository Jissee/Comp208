using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.Interface
{
    public interface IGonveranceManager
    {
        public IFieldList FieldList { get; }
        public IResourceList ResourceList { get; }
        public IPopulationManager PopManager { get; }
    }
}
