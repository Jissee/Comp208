using EoE.Server.GovernanceSystem;
using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace EoE.Test
    
{
    [TestClass]
    public class Gonverance_Unit_Test
    {
        [TestMethod]
        public void TestPopAllocation()
        {
            Server.Server Server = new Server.Server("0.0.0.0", 25566);
            var playerGonverance = new PlayerGonverance(Server);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            Assert.AreEqual(10, playerGonverance.FieldList.SiliconPop);
            Assert.AreEqual(10, playerGonverance.FieldList.CopperPop);
            Assert.AreEqual(10, playerGonverance.FieldList.IronPop);
            Assert.AreEqual(10, playerGonverance.FieldList.AluminumPop);
            Assert.AreEqual(10, playerGonverance.FieldList.IndustrailPop);
            Assert.AreEqual(10, playerGonverance.FieldList.ElectronicPop);
            Assert.AreEqual(40, playerGonverance.FieldList.AvailablePopulation);
            Assert.AreEqual(100, playerGonverance.TotalPopulation);
        }

        [TestMethod]
        public void TestPrimaryGeneration()
        {
            Server.Server Server = new Server.Server("0.0.0.0", 25566);
            var playerGonverance = new PlayerGonverance(Server);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            // new test
            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(12, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryAluminum.Count);

        }

        [TestMethod]
        public void TestSecondaryGeneration()
        {
            Server.Server Server = new Server.Server("0.0.0.0", 25566);
            var playerGonverance = new PlayerGonverance(Server);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(12, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryAluminum.Count);

            //new test
            playerGonverance.ProduceSecondaryResource();

            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryAluminum.Count);

        }

        public void TestPopulationDecrease()
        {
            Server.Server Server = new Server.Server("0.0.0.0", 25566);
            var playerGonverance = new PlayerGonverance(Server);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.FieldList.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(12, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(12, playerGonverance.ResourceList.CountryAluminum.Count);


            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(2, playerGonverance.ResourceList.CountryAluminum.Count);

        }

    }
}