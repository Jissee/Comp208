using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.Interface;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerGonveranceManager : IGonveranceManager
    {
        bool IsLose {  get; }
        PlayerStatus PlayerStatus{get;}
        IServerPopManager PopManager { get; }
        IServerFieldList FieldList { get; }
        IServerResourceList ResourceList { get; }
        void SetExploration(int inutPopulation);
        void SyntheticArmy(GameResourceType type, int count);
        void ClearAll();
    }
}
