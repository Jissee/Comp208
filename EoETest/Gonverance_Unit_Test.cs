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
using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
namespace EoE.Test
{
    [TestClass]
    public class Gonverance_Unit_Test
    {
        [TestMethod]
        public void TestPopAllocation()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            Assert.AreEqual(10, playerGonverance.PopManager.SiliconPop);
            Assert.AreEqual(10, playerGonverance.PopManager.CopperPop);
            Assert.AreEqual(10, playerGonverance.PopManager.IronPop);
            Assert.AreEqual(10, playerGonverance.PopManager.AluminumPop);
            Assert.AreEqual(10, playerGonverance.PopManager.IndustrailPop);
            Assert.AreEqual(10, playerGonverance.PopManager.ElectronicPop);
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, playerGonverance.PopManager.TotalPopulation);

            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 11);
            Assert.AreEqual(11, playerGonverance.PopManager.SiliconPop);
            Assert.AreEqual(39, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 9);
            Assert.AreEqual(9, playerGonverance.PopManager.SiliconPop);
            Assert.AreEqual(41, playerGonverance.PopManager.AvailablePopulation);
        }

        [TestMethod]
        public void TestPrimaryGeneration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            // new test
            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);

        }

        [TestMethod]
        public void TestSecondaryGeneration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);

            //new test
            playerGonverance.ProduceSecondaryResource();

            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryAluminum.Count);

        }
        [TestMethod]
        public void TestConsume()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);


            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryAluminum.Count);

            //new test
            int totalLack = playerGonverance.ConsumePrimaryResource();
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(240, totalLack);
        }

        [TestMethod]
        public void TestPopulationDecrease()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);


            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(40, playerGonverance.ResourceList.CountryAluminum.Count);

            int totalLack = playerGonverance.ConsumePrimaryResource();
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(5, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(240, totalLack);

            //new test
            playerGonverance.UpdatePopGrowthProgress(totalLack);

            Assert.AreEqual(-110, playerGonverance.PopGrowthProgress);
            playerGonverance.UpdatePop();
            Assert.AreEqual(-10, playerGonverance.PopGrowthProgress);
            Assert.AreEqual(99, playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(39, playerGonverance.PopManager.AvailablePopulation);
        }

        [TestMethod]
        public void TestPopulationIncrease()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Silicon, 25);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Copper, 25);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Iron, 25);
            playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Aluminum, 25);
            //playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Industrial, 10);
            //playerGonverance.PopManager.SetAllocation(GovernanceSystem.GameResourceType.Electronic, 10);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryAluminum.Count);


            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(125, playerGonverance.ResourceList.CountryAluminum.Count);

            int totalLack = playerGonverance.ConsumePrimaryResource();
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryElectronic.Count);
            Assert.AreEqual(0, playerGonverance.ResourceList.CountryIndustrial.Count);
            Assert.AreEqual(25, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(25, playerGonverance.ResourceList.CountryCopper.Count);
            Assert.AreEqual(25, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(25, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(0, totalLack);

            //new test
            playerGonverance.UpdatePopGrowthProgress(totalLack);

            Assert.AreEqual(110, playerGonverance.PopGrowthProgress);
            playerGonverance.UpdatePop();
            Assert.AreEqual(10, playerGonverance.PopGrowthProgress);
            Assert.AreEqual(101, playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(1, playerGonverance.PopManager.AvailablePopulation);
        }

        [TestMethod]
        public void TestSetExploration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Silicon, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Copper, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Iron, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Aluminum, 100));
            playerGonverance.SetExploration(10);
            Assert.AreEqual(10, playerGonverance.ExploratoinPopulation);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);
        }

        [TestMethod]
        public void TestExplorationProgress()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Silicon, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Copper, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Iron, 100));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Aluminum, 100));
            playerGonverance.SetExploration(10);
            Assert.AreEqual(10, playerGonverance.ExploratoinPopulation);
            Assert.AreEqual(10, playerGonverance.ExploratoinPopulation);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(50, playerGonverance.ResourceList.CountryCopper.Count);

            playerGonverance.UpdateFieldExplorationProgress();
            Assert.AreEqual(11, playerGonverance.FieldExplorationProgress);

            playerGonverance.UpdateField();
            Assert.AreEqual(120, playerGonverance.FieldList.TotalFieldCount);
        }

        [TestMethod]
        public void TestExploration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Silicon, 1000));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Copper, 1000));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Iron, 1000));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Aluminum, 1000));
            playerGonverance.SetExploration(100);
            Assert.AreEqual(100, playerGonverance.ExploratoinPopulation);
            Assert.AreEqual(500, playerGonverance.ResourceList.CountrySilicon.Count);
            Assert.AreEqual(500, playerGonverance.ResourceList.CountryIron.Count);
            Assert.AreEqual(500, playerGonverance.ResourceList.CountryAluminum.Count);
            Assert.AreEqual(500, playerGonverance.ResourceList.CountryCopper.Count);

            playerGonverance.UpdateFieldExplorationProgress();
            Assert.AreEqual(110, playerGonverance.FieldExplorationProgress);

            playerGonverance.UpdateField();
            Assert.AreEqual(121, playerGonverance.FieldList.TotalFieldCount);
        }

        [TestMethod]
        public void TestSyntheticArmy()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            server.BeginGame();
            var playerGonverance = new ServerPlayerGonverance(server.Status);
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Electronic, 19));
            playerGonverance.ResourceList.AddResource(new GovernanceSystem.ResourceStack(GovernanceSystem.GameResourceType.Industrial, 100));
            playerGonverance.SyntheticArmy(new BattleArmyStack(10));
            Assert.AreEqual(80, playerGonverance.PopManager.TotalPopulation);
            playerGonverance.SyntheticArmy(new InformativeArmyStack(10));
            Assert.AreEqual(60, playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(80, playerGonverance.ResourceList.CountryElectronic.Count);
            playerGonverance.SyntheticArmy(new MechanismArmyStack(10));
            Assert.AreEqual(40, playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(80, playerGonverance.ResourceList.CountryIndustrial.Count);

        }
    }
}