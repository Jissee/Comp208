using EoE.GovernanceSystem;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket;
using static EoE.GovernanceSystem.Interface.IGonveranceManager;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerGonvernance : ITickable, IServerGonveranceManager
    {
        public static readonly double EXPLORE_FIELD_PER_POP = 1.1f;
        public static readonly int FIELD_EXPLORE_THRESHOLD = 100;
        public static readonly int POP_GROWTH_THRESHOLD = 100;


        public int FieldExplorationProgress { get; private set; }

        public bool IsLose => PopManager.TotalPopulation <= 0 || FieldList.TotalFieldCount <= 0;
        public IServerFieldList FieldList { get; init; }
        public IServerResourceList ResourceList { get; init; }

        private IPlayer player;

        private GameStatus globalGameStatus;
        public PlayerStatus PlayerStatus { get; init; }
        public IServerPopManager PopManager { get; init; }

        public int PopGrowthProgress { get; private set; }

        public ServerPlayerGonvernance(GameStatus globalGameStatus, int initPop, IPlayer player)
        {
            this.globalGameStatus = globalGameStatus;
            this.PlayerStatus = new PlayerStatus(globalGameStatus);

            FieldList = new ServerPlayerFieldList(20, 20, 20, 20, 20, 20, player);
            ResourceList = new ServerPlayerResourceList((int)(7.5 * initPop), 6 * initPop, 9 * initPop, (int)(6.75 * initPop), 0, 0, 0, 0, 0);
            PopManager = new ServerPopulationManger(initPop, player);
            this.player = player;
        }

        private void ProducePrimaryResource()
        {
            ResourceStack resource = Recipes.calcSiliconP(
                PopManager.GetPopAllocCount(GameResourceType.Silicon),
                FieldList.GetFieldStack(GameResourceType.Silicon),
                0, 0);
            resource.Count = (int)PlayerStatus.CountrySiliconModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcCopperP(
                PopManager.GetPopAllocCount(GameResourceType.Copper),
                FieldList.GetFieldStack(GameResourceType.Copper),
                0, 0);
            resource.Count = (int)PlayerStatus.CountryCopperModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcIronP(
                PopManager.GetPopAllocCount(GameResourceType.Iron),
                FieldList.GetFieldStack(GameResourceType.Iron),
                0, 0);
            resource.Count = (int)PlayerStatus.CountryIronModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcAluminumP(
                PopManager.GetPopAllocCount(GameResourceType.Aluminum),
                FieldList.GetFieldStack(GameResourceType.Aluminum),
                0, 0);
            resource.Count = (int)PlayerStatus.CountryAluminumModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

        }
        private void ProduceSecondaryResource()
        {
            ResourceStack resource = Recipes.calcElectronicP(
                PopManager.GetPopAllocCount(GameResourceType.Electronic),
                FieldList.GetFieldStack(GameResourceType.Electronic),
                ResourceList.GetResourceCount(GameResourceType.Silicon),
                ResourceList.GetResourceCount(GameResourceType.Copper));
            resource.Count = (int)PlayerStatus.CountryElectronicModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);
            (ResourceStack consume1, ResourceStack consume2) = Recipes.calcElectronicPC(resource);
            ResourceList.SplitResourceStack(consume1);
            ResourceList.SplitResourceStack(consume2);


            resource = Recipes.calcIndustrailP(
                PopManager.GetPopAllocCount(GameResourceType.Industrial),
                FieldList.GetFieldStack(GameResourceType.Industrial),
                ResourceList.GetResourceCount(GameResourceType.Iron),
                ResourceList.GetResourceCount(GameResourceType.Aluminum));
            resource.Count = (int)PlayerStatus.CountryIndustralModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);
            (consume1, consume2) = Recipes.calcIndustrailPC(resource);
            ResourceList.SplitResourceStack(consume1);
            ResourceList.SplitResourceStack(consume2);
        }
        private void UpdatePopGrowthProgress()
        {

            PopGrowthProgress += Recipes.calcPopGrowthProgress(
                PopManager.TotalPopulation,
                ResourceList.GetResourceCount(GameResourceType.Silicon),
                ResourceList.GetResourceCount(GameResourceType.Copper),
                ResourceList.GetResourceCount(GameResourceType.Iron),
                ResourceList.GetResourceCount(GameResourceType.Aluminum));
        }
        private void UpdatePop()
        {
            if (Math.Abs(PopGrowthProgress) >= POP_GROWTH_THRESHOLD)
            {
                PopManager.AlterPop(PopGrowthProgress / POP_GROWTH_THRESHOLD);
                PopGrowthProgress %= POP_GROWTH_THRESHOLD;
            }
        }
        private void ConsumePrimaryResource(int population)
        {
            int pop = PopManager.TotalPopulation;

            int silionConsume = (int)(pop * Recipes.SILICON_PER_POP_TICK);
            int copperConsume = (int)(pop * Recipes.COPPER_PER_POP_TICK);
            int ironConsume = (int)(pop * Recipes.IRON_PER_POP_TICK);
            int aluminumConsume = (int)(pop * Recipes.ALUMINUM_PER_POP_TICK);

            ResourceList.SplitResource(GameResourceType.Silicon, silionConsume);
            ResourceList.SplitResource(GameResourceType.Copper, copperConsume);
            ResourceList.SplitResource(GameResourceType.Iron, ironConsume);
            ResourceList.SplitResource(GameResourceType.Aluminum, aluminumConsume);
        }

        public void SetExploration(int inutPopulation)
        {
            if (inutPopulation < 0)
            {
                player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                player.SendPacket(new ServerMessagePacket("Negative input"));
                player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
            }
            if (inutPopulation > PopManager.AvailablePopulation)
            {
                player.SendPacket(new ServerMessagePacket("No enough available population"));
                player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
            }
            else
            {
                int consume = inutPopulation * EXPLORE_RESOURCE_PER_POP;
                if (ResourceList.GetResourceCount(GameResourceType.Silicon) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Copper) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Iron) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Aluminum) >= consume)
                {
                    ResourceList.SplitResource(GameResourceType.Silicon, consume);
                    ResourceList.SplitResource(GameResourceType.Copper, consume);
                    ResourceList.SplitResource(GameResourceType.Iron, consume);
                    ResourceList.SplitResource(GameResourceType.Aluminum, consume);
                    PopManager.SetExploration(inutPopulation);
                    player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                    player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
                }
                else
                {
                    player.SendPacket(new ServerMessagePacket("No enough resources"));
                }

            }
        }

        private void UpdateFieldExplorationProgress()
        {
            if (PopManager.ExploratoinPopulation > 0)
            {
                FieldExplorationProgress += (int)(PopManager.ExploratoinPopulation * EXPLORE_FIELD_PER_POP);
                PopManager.AlterPop(PopManager.ExploratoinPopulation);
                PopManager.SetExploration(0);
            }
        }
        private void UpdateField()
        {
            int exploredFieldCount = FieldExplorationProgress / FIELD_EXPLORE_THRESHOLD;
            int acutalExplored = Math.Min(exploredFieldCount, globalGameStatus.UnidentifiedField);
            FieldExplorationProgress -= acutalExplored * FIELD_EXPLORE_THRESHOLD;
            globalGameStatus.UnidentifiedField -= acutalExplored;

            if (acutalExplored > 0)
            {
                for (int i = 0; i < acutalExplored; i++)
                {
                    Random random = new Random();
                    GameResourceType type = (GameResourceType)random.Next(4);
                    FieldList.AddField(type, 1);
                }
            }
        }
        public void SyntheticArmy(GameResourceType type, int count)
        {
            if ((int)type < (int)GameResourceType.BattleArmy)
            {

            }

            ResourceStack army = new ResourceStack(type, count);
            int popCount;
            ResourceStack resource;
            switch (type)
            {
                case GameResourceType.BattleArmy:
                    (popCount, resource) = Recipes.BattleArmyproduce(army);

                    if (popCount <= PopManager.AvailablePopulation)
                    {
                        PopManager.AlterPop(-popCount);
                        ResourceList.AddResourceStack(army);
                        player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                        player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
                    }
                    else
                    {
                        player.SendPacket(new ServerMessagePacket("No enough available population"));
                    }
                    break;
                case GameResourceType.InformativeArmy:
                    (popCount, resource) = Recipes.produceInfomativeArmy(army);
                    if (popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Electronic))
                    {
                        PopManager.AlterPop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                        player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                        player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
                    }
                    else
                    {
                        player.SendPacket(new ServerMessagePacket("No enough available population or resources"));
                    }
                    break;
                case GameResourceType.MechanismArmy:
                    (popCount, resource) = Recipes.produceMechanismArmy(army);
                    if (popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Industrial))
                    {
                        PopManager.AlterPop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                        player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                        player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
                    }
                    else
                    {
                        player.SendPacket(new ServerMessagePacket("No enough available population or resources"));
                    }
                    break;
                default:
                    throw new Exception("no such type");
            }

        }
        public void ClearAll()
        {
            FieldList.ClearAll();
            ResourceList.ClearAll();
            PopManager.ClearAll();
        }


        public void Tick()
        {
            ProducePrimaryResource();
            ProduceSecondaryResource();
            int pop = PopManager.TotalPopulation;
            UpdatePopGrowthProgress();
            UpdatePop();
            ConsumePrimaryResource(pop);
            UpdateFieldExplorationProgress();
            UpdateField();
            player.SendPacket(new ResourceUpdatePacket(ResourceList.GetResourceListRecord()));
            player.SendPacket(new FieldUpdatePacket(FieldList.GetFieldListRecord()));
            player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
        }
    }
}
