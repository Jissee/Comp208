using System;
using EoE.GovernanceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EoE.Server.WarSystem;
using System.Diagnostics;
using EoE.Network;
using EoE.WarSystem;
using EoE.GovernanceSystem.Interface;
using static EoE.GovernanceSystem.Interface.IGonveranceManager;
using EoE.GovernanceSystem.ServerInterface;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Entities;

namespace EoE.Server.GovernanceSystem
{
    public class ServerPlayerGonverance : ITickable, IServerGonveranceManager
    {
        //consume rate
        public static readonly double SILICON_PER_POP_TICK = 1.0f;
        public static readonly double COPPER_PER_POP_TICK = 1.0f;
        public static readonly double IRON_PER_POP_TICK = 1.0f;
        public static readonly double ALUMINUM_PER_POP_TICK = 1.0f;

        public static readonly double EXPLORE_FIELD_PER_POP = 1.1f;
        public static readonly int FIELD_EXPLORE_THRESHOLD = 100;

        public static readonly int POP_GROWTH_THRESHOLD = 100;

        
        public int FieldExplorationProgress { get; private set; }

        public bool IsLose => PopManager.TotalPopulation <= 0 || FieldList.TotalFieldCount <= 0;
        public IServerFieldList FieldList { get; init; }
        public IServerResourceList ResourceList { get; init; }

        private IPlayer player;
        private IServer server;

        private GameStatus globalGameStatus;
        private PlayerStatus playerStatus;
        public IServerPopManager PopManager { get; init; }
        
        public int PopGrowthProgress { get; private set;}

        public ServerPlayerGonverance(GameStatus globalGameStatus, int initPop,IPlayer player, IServer server)
        {
            this.globalGameStatus = globalGameStatus;
            this.playerStatus = new PlayerStatus(globalGameStatus);

            FieldList = new ServerPlayerFieldList();
            ResourceList = new ServerPlayerResourceList();
            PopManager = new ServerPopulationManger(initPop, player);
            this.player = player;
            this.server = server;
        }

        // 暂时改为public！！！
        public void ProducePrimaryResource()
        {
            ResourceStack resource = Recipes.calcPrimaryP(
                PopManager.GetPopAllocCount(GameResourceType.Silicon),
                FieldList.GetFieldStack(GameResourceType.Silicon), 
                0, 0);
            resource.Count = (int)playerStatus.CountrySiliconModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcPrimaryP(
                PopManager.GetPopAllocCount(GameResourceType.Copper),
                FieldList.GetFieldStack(GameResourceType.Copper), 
                0, 0);
            resource.Count = (int)playerStatus.CountryCopperModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcPrimaryP(
                PopManager.GetPopAllocCount(GameResourceType.Iron),
                FieldList.GetFieldStack(GameResourceType.Iron),
                0, 0);
            resource.Count = (int)playerStatus.CountryIronModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

            resource = Recipes.calcPrimaryP(
                PopManager.GetPopAllocCount(GameResourceType.Aluminum),
                FieldList.GetFieldStack(GameResourceType.Aluminum), 
                0, 0);
            resource.Count = (int)playerStatus.CountryAluminumModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);

        }

        // 暂时改为public！！！
        public void ProduceSecondaryResource()
        {
            ResourceStack resource = Recipes.calcElectronicP(
                PopManager.GetPopAllocCount(GameResourceType.Electronic), 
                FieldList.GetFieldStack(GameResourceType.Electronic), 
                ResourceList.GetResourceCount(GameResourceType.Silicon), 
                ResourceList.GetResourceCount(GameResourceType.Copper));
            resource.Count = (int)playerStatus.CountryElectronicModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);
            (ResourceStack consume1, ResourceStack consume2) = Recipes.calcElectronicPC(resource);
            ResourceList.SplitResourceStack(consume1);
            ResourceList.SplitResourceStack(consume2);


            resource = Recipes.calcIndustrailP(
                PopManager.GetPopAllocCount(GameResourceType.Industrial),
                FieldList.GetFieldStack(GameResourceType.Industrial),
                ResourceList.GetResourceCount(GameResourceType.Iron),
                ResourceList.GetResourceCount(GameResourceType.Aluminum));
            resource.Count = (int)playerStatus.CountryIndustryModifier.Apply(resource.Count);
            ResourceList.AddResourceStack(resource);
            (consume1, consume2) = Recipes.calcIndustrailPC(resource);
            ResourceList.SplitResourceStack(consume1);
            ResourceList.SplitResourceStack(consume2);
        }

        // 暂时改为public！！！
        public void UpdatePopGrowthProgress()
        {
            
            PopGrowthProgress += Recipes.calcPopGrowthProgress(
                PopManager.TotalPopulation,
                ResourceList.GetResourceCount(GameResourceType.Silicon),
                ResourceList.GetResourceCount(GameResourceType.Copper),
                ResourceList.GetResourceCount(GameResourceType.Iron),
                ResourceList.GetResourceCount(GameResourceType.Aluminum));
        }
        // 暂时改为public！！！
        public void UpdatePop()
        {
            if (Math.Abs(PopGrowthProgress)>= POP_GROWTH_THRESHOLD)
            {
                PopManager.AlterPop(PopGrowthProgress / POP_GROWTH_THRESHOLD);
                PopGrowthProgress %= POP_GROWTH_THRESHOLD;
            }
        }
        // 暂时改为public！！！
        public void ConsumePrimaryResource(int population)
        {
            int pop = PopManager.TotalPopulation;
            
            int silionConsume = (int)(pop * SILICON_PER_POP_TICK);
            int copperConsume = (int)(pop * COPPER_PER_POP_TICK);
            int ironConsume = (int)(pop * IRON_PER_POP_TICK);
            int aluminumConsume = (int)(pop * ALUMINUM_PER_POP_TICK);

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
            }
            if (inutPopulation > PopManager.AvailablePopulation)
            {
                player.SendPacket(new ServerMessagePacket("No enough available population"));
                player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
            }
            else 
            {
                int consume = inutPopulation * EXPLORE_RESOURCE_PER_POP;
                if (ResourceList.GetResourceCount(GameResourceType.Silicon) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Copper) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Iron) >= consume &&
                    ResourceList.GetResourceCount(GameResourceType.Aluminum) >= consume)
                {
                    ResourceList.SplitResource(GameResourceType.Silicon,consume);
                    ResourceList.SplitResource(GameResourceType.Copper, consume);
                    ResourceList.SplitResource(GameResourceType.Iron, consume);
                    ResourceList.SplitResource(GameResourceType.Aluminum, consume);
                    PopManager.SetExploration(inutPopulation);
                }
                else
                {
                    player.SendPacket(new ServerMessagePacket("No enough resources"));
                    player.SendPacket(new PopulationUpdatePacket(PopManager.GetPopulationRecord()));
                }

            }
        }

        // 暂时改为public！！！
        public void UpdateFieldExplorationProgress()
        {
            if (PopManager.ExploratoinPopulation > 0)
            {
                FieldExplorationProgress += (int)(PopManager.ExploratoinPopulation * EXPLORE_FIELD_PER_POP);
                PopManager.AlterPop(PopManager.ExploratoinPopulation);
                PopManager.SetExploration(0);
            }
        }

        // 暂时改为public！！！
        public void UpdateField()
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
                    GameResourceType type = (GameResourceType) random.Next(4);
                    FieldList.AddField(type, 1);
                }
            }
        }
        public void SyntheticArmy(GameResourceType type,int count)
        {
            if ((int)type < (int)GameResourceType.BattleArmy )
            {
                
            }

            ResourceStack army = new ResourceStack(type,count);
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
                    }
                    else
                    {
                        throw new InvalidPopAllocException();
                    }
                    break;
                case GameResourceType.InformativeArmy:
                    (popCount, resource) = Recipes.produceInfomativeArmy(army);
                    if (popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Electronic))
                    {
                        PopManager.AlterPop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                    }
                    else
                    {
                        throw new InvalidPopAllocException();
                    }
                    break;
                case GameResourceType.MechanismArmy:
                    (popCount, resource) = Recipes.produceMechanismArmy(army);
                    if(popCount <= PopManager.AvailablePopulation && resource.Count <= ResourceList.GetResourceCount(GameResourceType.Industrial))
                    {
                        PopManager.AlterPop(-popCount);
                        ResourceList.SplitResourceStack(resource);
                        ResourceList.AddResourceStack(army);
                    }
                    else
                    {
                        throw new InvalidPopAllocException();
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
            //Todo
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
 