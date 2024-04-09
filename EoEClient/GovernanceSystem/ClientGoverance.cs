using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.GovernanceSystem
{
    public class ClientGoverance: IClietGonveranceManager
    {
        public ClientFieldList FieldList { get; init; }
        public ClientResourceList ResourceList { get; init; }
        public ClientPopulationManager PopManager { get; init; }

        IFieldList IGonveranceManager.FieldList => FieldList;

        IResourceList IGonveranceManager.ResourceList => ResourceList;

        IPopulationManager IClietGonveranceManager.PopManager => PopManager;

        public ClientGoverance()
        {
            FieldList = new ClientFieldList();
            ResourceList = new ClientResourceList();
            PopManager = new ClientPopulationManager();
        }

        public void SetExploration(int inutPopulation)
        {
            throw new NotImplementedException();
        }
    }
}
