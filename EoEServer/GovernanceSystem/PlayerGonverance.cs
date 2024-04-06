using System;
using EoE.GovernanceSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EoE.Server.WarSystem;
using System.Diagnostics;

namespace EoE.Server.GovernanceSystem
{
    public class PlayerGonverance : ITickable
    {
        public readonly double SILICON_PER_POP_TICK = 1.0f;
        public readonly double COPPER_PER_POP_TICK = 1.0f;
        public readonly double IRON_PER_POP_TICK = 1.0f;
        public readonly double ALUMINUM_PER_POP_TICK = 1.0f;

        public readonly int EXPLORE_RESOURCE_PER_POP = 5;
        public readonly double EXPLORE_FIELD_PER_POP = 1.1f;
        public int? ExploratoinPopulation { get; private set;}
        public int FieldExplorationProgress { get; private set; }
        public static readonly int FIELD_EXPLORE_THRESHOLD = 100;

        public bool IsLose => TotalPopulation <= 0 || FieldList.TotalFieldCount <= 0;
        public PlayerFieldList FieldList { get; }
        public PlayerResourceList ResourceList { get; }

        private Server server;


        public Modifier CountryResourceModifier { get; init; }
        public Modifier CountryPrimaryModifier { get; init; }
        public Modifier CountrySecondaryModifier { get; init; }
        public Modifier CountrySiliconModifier { get; init; }
        public Modifier CountryCopperModifier { get; init; }
        public Modifier CountryIronModifier { get; init; }
        public Modifier CountryAluminumModifier { get; init; }
        public Modifier CountryElectronicModifier { get; init; }
        public Modifier CountryIndustryModifier { get; init; }

        public int TotalPopulation => FieldList.TotalPopulation;
        public static readonly int POP_GROWTH_THRESHOLD = 100;
        public int PopGrowthProgress { get; private set;}
        public PlayerGonverance(Server server)
        {
            this.server = server;

            FieldList = new PlayerFieldList();
            ResourceList = new PlayerResourceList();

            CountryResourceModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryPrimaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySecondaryModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountrySiliconModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryCopperModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryIronModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryAluminumModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryElectronicModifier = new Modifier("", Modifier.ModifierType.Plus);
            CountryIndustryModifier = new Modifier("", Modifier.ModifierType.Plus);
        }

        public void PrepareModifier(Server server)
        {
            CountrySiliconModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalSiliconModifier)
                .AddNode(server.GlobalPrimaryModifier)
                .AddNode(server.GlobalResourceModifier);

            CountryCopperModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalCopperModifier)
                .AddNode(server.GlobalPrimaryModifier)
                .AddNode(server.GlobalResourceModifier);

            CountryIronModifier
                .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalIronModifier)
                .AddNode(server.GlobalPrimaryModifier)
                .AddNode(server.GlobalResourceModifier);

            CountryAluminumModifier
               .AddNode(CountryPrimaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalAluminumModifier)
                .AddNode(server.GlobalPrimaryModifier)
                .AddNode(server.GlobalResourceModifier);

            CountryElectronicModifier
               .AddNode(CountrySecondaryModifier)
                .AddNode(CountryResourceModifier)
                .AddNode(server.GlobalElectronicModifier)
                .AddNode(server.GlobalSecondaryModifier)
                .AddNode(server.GlobalResourceModifier);

            CountryIndustryModifier
              .AddNode(CountrySecondaryModifier)
               .AddNode(CountryResourceModifier)
               .AddNode(server.GlobalIndustryModifier)
               .AddNode(server.GlobalSecondaryModifier)
               .AddNode(server.GlobalResourceModifier);
        }

        // 暂时改为public！！！
        public void ProducePrimaryResource()
        {
            ResourceStack resource = Recipes.producePrimaryResource(FieldList.SiliconPop, FieldList.CountryFieldSilicon, null, null);
            resource.Count = (int)CountrySiliconModifier.Apply(resource.Count);
            ResourceList.CountrySilicon.Add(resource);

            resource = Recipes.producePrimaryResource(FieldList.CopperPop, FieldList.CountryFieldCopper, null, null);
            resource.Count = (int)CountryCopperModifier.Apply(resource.Count);
            ResourceList.CountryCopper.Add(resource);

            resource = Recipes.producePrimaryResource(FieldList.IronPop, FieldList.CountryFieldIron, null, null);
            resource.Count = (int)CountryIronModifier.Apply(resource.Count);
            ResourceList.CountryIron.Add(resource);

            resource = Recipes.producePrimaryResource(FieldList.AluminumPop, FieldList.CountryFieldAluminum, null, null);
            resource.Count = (int)CountryAluminumModifier.Apply(resource.Count);
            ResourceList.CountryAluminum.Add(resource);

        }

        // 暂时改为public！！！
        public void ProduceSecondaryResource()
        {
            ResourceStack resource = Recipes.produceElectronic(FieldList.ElectronicPop, FieldList.CountryFieldElectronic, ResourceList.CountrySilicon, ResourceList.CountryCopper);
            resource.Count = (int)CountryElectronicModifier.Apply(resource.Count);
            ResourceList.CountryElectronic.Add(resource);
            resource = Recipes.produceIndustry(FieldList.IndustrailPop, FieldList.CountryFieldIndustry, ResourceList.CountryIron, ResourceList.CountryAluminum);
            resource.Count = (int)CountryIndustryModifier.Apply(resource.Count);
            ResourceList.CountryIndustrial.Add(resource);
        }

        private void UpdatePopGrowthProgress(int totalLack)
        {
            int surplus = 0;
            int count = FieldList.TotalPopulation;
            if (totalLack > 0)
            {
                surplus = -totalLack;
            }
            else
            {
                surplus = ResourceList.CountrySilicon.Count + ResourceList.CountryIron.Count + ResourceList.CountryCopper.Count + ResourceList.CountryAluminum.Count;
            }

            PopGrowthProgress += Recipes.calcPopGrowthProgress(TotalPopulation, surplus);
        }
        private void UpdatePop()
        {
            if (Math.Abs(PopGrowthProgress)>= POP_GROWTH_THRESHOLD)
            {
                FieldList.PopGrow(PopGrowthProgress / POP_GROWTH_THRESHOLD);
                PopGrowthProgress %= POP_GROWTH_THRESHOLD;
            }
        }
        private int ConsumePrimaryResource()
        {
            int pop = FieldList.TotalPopulation;
            
            int silionConsume = (int)(pop * SILICON_PER_POP_TICK);
            int copperConsume = (int)(pop * COPPER_PER_POP_TICK);
            int ironConsume = (int)(pop * IRON_PER_POP_TICK);
            int aluminumConsume = (int)(pop * ALUMINUM_PER_POP_TICK);

            int totalLack = 0;
            totalLack += CheckLackAndConsume(ResourceList.CountrySilicon, silionConsume);
            totalLack += CheckLackAndConsume(ResourceList.CountryCopper, copperConsume);
            totalLack += CheckLackAndConsume(ResourceList.CountryIron, ironConsume);
            totalLack += CheckLackAndConsume(ResourceList.CountryAluminum, aluminumConsume);
            return totalLack;
        }
        private int CheckLackAndConsume(ResourceStack resource, int consume)
        {

            if (resource.Count >= consume)
            {
                resource.Count -= consume;
                return 0;
            }
            else
            {
                int lack = consume - resource.Count;
                resource.Count = 0;
                return lack;
            }
        }

        public void SetExploration(int inutPopulation)
        {
            if (inutPopulation > FieldList.AvailablePopulation)
            {
                throw new InvalidPopAllocException();
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
                }
                else
                {
                    throw new InvalidPopAllocException();
                }

                ExploratoinPopulation = inutPopulation;
            }
        }

        private void UpdateFieldExplorationProgress()
        {
            if (ExploratoinPopulation != null)
            {
                FieldExplorationProgress += (int)(ExploratoinPopulation * EXPLORE_FIELD_PER_POP);
            }
        }

        private void UpdateField()
        {
            int exploredFieldCount = FieldExplorationProgress / FIELD_EXPLORE_THRESHOLD;
            int acutalExplored = Math.Min(exploredFieldCount, server.UnidentifiedField);
            FieldExplorationProgress -= acutalExplored * FIELD_EXPLORE_THRESHOLD;
            server.UnidentifiedField -= acutalExplored;

            if (acutalExplored > 0)
            {
                for (int i = 0; i < acutalExplored; i++)
                {
                    Random random = new Random();
                    GameResourceType type = (GameResourceType) random.Next(4);
                    switch (type)
                    {
                        case GameResourceType.Silicon:
                            FieldList.addField(new FieldStack(GameResourceType.Silicon, 1));
                            break;
                        case GameResourceType.Copper:
                            FieldList.addField(new FieldStack(GameResourceType.Copper, 1));
                            break;
                        case GameResourceType.Iron:
                            FieldList.addField(new FieldStack(GameResourceType.Iron, 1));
                            break;
                        case GameResourceType.Aluminum:
                            FieldList.addField(new FieldStack(GameResourceType.Aluminum, 1));
                            break;
                        default:
                            throw new Exception("no such type");
                    }
                }
            }
        }
        public void SyntheticArmy(ArmyStack army)
        {
            GameResourceType type = army.Type;
            int popCount;
            ResourceStack resource;
            switch (type)
            {
                case GameResourceType.BattleArmy:
                    (popCount, resource) = Recipes.BattleArmyproduce(army);
                    if (popCount >= FieldList.AvailablePopulation)
                    {
                        ResourceList.AddResource(army);
                    }
                    else
                    {
                        throw new InvalidPopAllocException();
                    }
                    break;
                case GameResourceType.InformativeArmy:
                    (popCount, resource) = Recipes.produceInfomativeArmy(army);
                    if (popCount >= FieldList.AvailablePopulation && resource.Count <= ResourceList.CountryElectronic.Count)
                    {
                        ResourceList.AddResource(army);
                    }
                    else
                    {
                        throw new InvalidPopAllocException();
                    }
                    break;
                case GameResourceType.MechanismArmy:
                    (popCount, resource) = Recipes.produceMechanismArmy(army);
                    if(popCount >= FieldList.AvailablePopulation && resource.Count <= ResourceList.CountryIndustrial.Count)
                    {
                        ResourceList.AddResource(army);
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

        public void Tick()
        {
            ProducePrimaryResource();
            ProduceSecondaryResource();
            int totalLack = ConsumePrimaryResource();
            UpdatePopGrowthProgress(totalLack);
            UpdatePop();
            UpdateFieldExplorationProgress();
            UpdateField();
        }
    }
}
