using EoE.GovernanceSystem;
using EoE.WarSystem.Interface;

namespace EoE.Server.WarSystem
{
    public class Army : IArmy
    {
        private IWarParty warParty;
        public static readonly BattleArmyInfo battleArmyInfo = new BattleArmyInfo();
        public static readonly MechanismArmyInfo mechanismArmyInfo = new MechanismArmyInfo();
        public static readonly InformativeArmyInfo informativeArmyInfo = new InformativeArmyInfo();
        public Dictionary<GameResourceType, int> armyStacks;
        public int Consumption { get; private set; }
        public Army(IWarParty warParty)
        {
            this.warParty = warParty;
            armyStacks = new Dictionary<GameResourceType, int>
            {
                {GameResourceType.BattleArmy,0 },
                {GameResourceType.MechanismArmy,0 },
                {GameResourceType.InformativeArmy,0 }
            };
            Consumption = 0;
        }
        public void Clear()
        {
            armyStacks[GameResourceType.BattleArmy] = 0;
            armyStacks[GameResourceType.InformativeArmy] = 0;
            armyStacks[GameResourceType.MechanismArmy] = 0;

        }
        public void AddBattle(ResourceStack battle)
        {
            if (battle.Type == GameResourceType.BattleArmy)
            {
                armyStacks[GameResourceType.BattleArmy] += battle.Count;
            }
            else
            {
                throw new Exception("Wrong type");
            }

        }
        public void AddMechanism(ResourceStack mechanism)
        {
            if (mechanism.Type == GameResourceType.MechanismArmy)
            {
                armyStacks[GameResourceType.MechanismArmy] += mechanism.Count;
            }
            else
            {
                throw new Exception("Wrong type");
            }
        }

        public void AddInformative(ResourceStack informative)
        {
            if (informative.Type == GameResourceType.InformativeArmy)
            {
                armyStacks[GameResourceType.InformativeArmy] += informative.Count;
            }
            else
            {
                throw new Exception("Wrong type");
            }
        }
        public void DecreaseBattle(int count)
        {
            int original = armyStacks[GameResourceType.BattleArmy];
            int decreasing = Math.Min(original, count);
            Consumption += decreasing * battleArmyInfo.Worth;
            int newCount = original - decreasing;
            armyStacks[GameResourceType.BattleArmy] = newCount;
        }
        public void DecreaseMechanism(int count)
        {
            int original = armyStacks[GameResourceType.MechanismArmy];
            int decreasing = Math.Min(original, count);
            Consumption += decreasing * mechanismArmyInfo.Worth;
            int newCount = original - decreasing;
            armyStacks[GameResourceType.MechanismArmy] = newCount;
        }
        public void DecreaseInformative(int count)
        {
            int original = armyStacks[GameResourceType.InformativeArmy];
            int decreasing = Math.Min(original, count);
            Consumption += decreasing * informativeArmyInfo.Worth;
            int newCount = original - decreasing;
            armyStacks[GameResourceType.InformativeArmy] = newCount;
        }
        public void AddConsumption(int count)
        {
            Consumption += count;
        }

        public int CalculateMechaAttack()
        {
            int totalMechaAttack = 0;
            totalMechaAttack += armyStacks[GameResourceType.MechanismArmy] * mechanismArmyInfo.MechanAttack;
            totalMechaAttack += armyStacks[GameResourceType.InformativeArmy] * informativeArmyInfo.MechanAttack;
            totalMechaAttack += armyStacks[GameResourceType.BattleArmy] * battleArmyInfo.MechanAttack;
            return totalMechaAttack;
        }
        public int CalculateMechaDefense()
        {
            int totalMechaDefense = 0;
            totalMechaDefense += armyStacks[GameResourceType.MechanismArmy] * mechanismArmyInfo.MechanDefense;
            totalMechaDefense += armyStacks[GameResourceType.InformativeArmy] * informativeArmyInfo.MechanDefense;
            totalMechaDefense += armyStacks[GameResourceType.BattleArmy] * battleArmyInfo.MechanDefense;
            return totalMechaDefense;
        }
        public int CalculateBattleAttack()
        {
            int totalBattleAttack = 0;
            totalBattleAttack += armyStacks[GameResourceType.MechanismArmy] * mechanismArmyInfo.BattleAttack;
            totalBattleAttack += armyStacks[GameResourceType.InformativeArmy] * informativeArmyInfo.BattleAttack;
            totalBattleAttack += armyStacks[GameResourceType.BattleArmy] * battleArmyInfo.BattleAttack;
            return totalBattleAttack;
        }

        public int GetBattleCount()
        {
            return armyStacks[GameResourceType.BattleArmy];
        }

        public int GetInformativeCount()
        {
            return armyStacks[GameResourceType.InformativeArmy];
        }

        public int GetMechanismCount()
        {
            return armyStacks[GameResourceType.MechanismArmy];
        }
    }
}
