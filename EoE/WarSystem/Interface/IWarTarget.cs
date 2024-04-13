using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IWarTarget
    {
        List<ResourceStack> ResourceClaim { get; }
        int FieldClaim { get; }
        int PopClaim { get; } 
    }
}
