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
using EoE.Server;
using EoE.GovernanceSystem.ServerInterface;
namespace EoE.Test
{
    [TestClass]
    public class Gonverance_Unit_Test
    {
        [TestMethod]
        public void TestPopAllocation()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            IServerGonveranceManager playerGonverance = player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(10, 10, 10, 10, 10, 10);

            
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.PopManager.SetAllocation(15, 15, 10, 10, 10, 10);
            Assert.AreEqual(15, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(15, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(30, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);



        }

        [TestMethod]
        public void TestPrimaryGeneration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(10, 10, 10, 10, 10, 10);


            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

        }
        [TestMethod]
        public void TestSecondaryGeneration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(10, 10, 10, 10, 10, 10);


            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));
        }

        [TestMethod]
        public void TestConsume()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(10, 10, 10, 10, 10, 10);


            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            playerGonverance.ConsumePrimaryResource(playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));
        }

        [TestMethod]
        public void TestConsumeEnough()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(25, 25, 25, 25, 0, 0);


            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(0, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            playerGonverance.ConsumePrimaryResource(playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));
        }

        [TestMethod]
        public void TestPopulationProgress()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(25, 25, 25, 25, 0, 0);


            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(0, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            int pop = playerGonverance.PopManager.TotalPopulation;
            playerGonverance.UpdatePopGrowthProgress();
            Assert.AreEqual(20, playerGonverance.PopGrowthProgress);

            playerGonverance.ConsumePrimaryResource(pop);
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            
        }

        [TestMethod]
        public void TestPopulationGrowth()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(25, 25, 25, 25, 0, 0);


            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(0, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            int pop = playerGonverance.PopManager.TotalPopulation;
            playerGonverance.UpdatePopGrowthProgress();
            Assert.AreEqual(20, playerGonverance.PopGrowthProgress);
            playerGonverance.UpdatePop();
            Assert.AreEqual(100, playerGonverance.PopManager.TotalPopulation);
            Assert.AreEqual(20, playerGonverance.PopGrowthProgress);

            playerGonverance.ConsumePrimaryResource(pop);
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(25, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(0, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));


        }

        [TestMethod]
        public void TestExploration()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(20, 20, 25, 25, 0, 0);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(100, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(100, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.SetExploration(10);
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(75, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(75, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(90, playerGonverance.PopManager.TotalPopulation);
        }

        [TestMethod]
        public void TestFindField()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(20, 20, 25, 25, 0, 0);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(100, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(100, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(125, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.SetExploration(10);
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(75, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(75, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(0, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(90, playerGonverance.PopManager.TotalPopulation);

            Assert.AreEqual(10, playerGonverance.PopManager.ExploratoinPopulation);
            playerGonverance.UpdateFieldExplorationProgress();
            Assert.AreEqual(11, playerGonverance.FieldExplorationProgress);
            Assert.AreEqual(10, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(0, playerGonverance.PopManager.ExploratoinPopulation);
            playerGonverance.UpdateField();
            Assert.AreEqual(11, playerGonverance.FieldExplorationProgress);
            Assert.AreEqual(120, playerGonverance.FieldList.TotalFieldCount);
        }

        [TestMethod]
        public void TestSyntheticArmy()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();

            ServerPlayerGonverance playerGonverance = (ServerPlayerGonverance)player.GonveranceManager;
            Assert.AreEqual(100, playerGonverance.PopManager.AvailablePopulation);
            playerGonverance.PopManager.SetAllocation(10, 10, 10, 10, 10, 10);


            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Silicon));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Copper));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Iron));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Aluminum));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Electronic));
            Assert.AreEqual(10, playerGonverance.PopManager.GetPopAllocCount(GameResourceType.Industrial));
            Assert.AreEqual(40, playerGonverance.PopManager.AvailablePopulation);
            Assert.AreEqual(100, player.GonveranceManager.PopManager.TotalPopulation);

            playerGonverance.ProducePrimaryResource();
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(50, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));

            playerGonverance.ProduceSecondaryResource();
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Silicon));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Copper));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Iron));
            Assert.AreEqual(40, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Aluminum));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            Assert.AreEqual(5, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));

            playerGonverance.SyntheticArmy(GameResourceType.BattleArmy,5);
            Assert.AreEqual(90, playerGonverance.PopManager.TotalPopulation);
            playerGonverance.SyntheticArmy(GameResourceType.InformativeArmy, 2);
            Assert.AreEqual(1, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Electronic));
            playerGonverance.SyntheticArmy(GameResourceType.MechanismArmy, 2);
            Assert.AreEqual(1, playerGonverance.ResourceList.GetResourceCount(GameResourceType.Industrial));
        }
    }
}
