using EoE.GovernanceSystem;
using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EoE.GovernanceSystem.Interface.IGonveranceManager;

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
            if (inutPopulation < 0)
            {
                Client.INSTANCE.MsgBox("Negative input");
            }
            if (inutPopulation > PopManager.AvailablePopulation)
            {
                Client.INSTANCE.MsgBox("Insufficient available population");
            }
            else
            {
                int consume = inutPopulation * EXPLORE_RESOURCE_PER_POP;

                if (ResourceList.CountrySilicon.Count >= consume && ResourceList.CountryCopper.Count >= consume &&
                    ResourceList.CountryAluminum.Count >= consume && ResourceList.CountryIron.Count >= consume)
                {
                    ResourceList.CountrySilicon.Count -= consume;
                    ResourceList.CountryCopper.Count -= consume;
                    ResourceList.CountryAluminum.Count -= consume;
                    ResourceList.CountryIron.Count -= consume;
                    Client.INSTANCE.SendPacket(new SetExplorationPacket(inutPopulation));
                }
                else
                {
                    Client.INSTANCE.MsgBox("Insufficient resources");
                }
            }
        }



        
    }
}
