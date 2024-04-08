﻿using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.GovernanceSystem
{
    public class ClientGoverance:IGonveranceManager
    {
        public ClientFieldList FieldList { get; init; }
        public ClientResourceList ResourceList { get; init; }
        public ClientPopulationManager PopManager { get; init; }

        IFieldList IGonveranceManager.FieldList => FieldList;

        IResourceList IGonveranceManager.ResourceList => ResourceList;

        IPopulationManager IGonveranceManager.PopManager => PopManager;

        public ClientGoverance()
        {
            FieldList = new ClientFieldList();
            ResourceList = new ClientResourceList();
            PopManager = new ClientPopulationManager();
        }
    }
}
