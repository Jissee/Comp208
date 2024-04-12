﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.SrverInterface
{
    public interface IServerPopManager : IPopManager
    {
        int ExploratoinPopulation { get; }
        void SetAllocation(int siliconPop, int copperPop, int ironPop, int aluminumPop, int industrialPop, int electronic);
        void AlterPop(int count);
        void SetExploration(int population);
    }
}
