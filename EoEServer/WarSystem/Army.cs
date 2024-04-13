using EoE.GovernanceSystem;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class Army : IArmy
    {
        private IWarParty warParty;
        public static readonly BattleArmyInfo battleArmyInfo = new BattleArmyInfo();
        public static readonly MechanismArmyInfo mechanismArmyInfo = new MechanismArmyInfo();
        public static readonly InformativeArmyInfo informativeArmyInfo = new InformativeArmyInfo();
        public ResourceStack Battle { get; set; }
        public ResourceStack Informative { get; set; }
        public ResourceStack Mechanism { get; set; }
        public int Consumption { get; private set; }
        public Army(IWarParty warParty) 
        {
            this.warParty = warParty;
            Battle = new ResourceStack(GameResourceType.BattleArmy, 0);
            Informative = new ResourceStack(GameResourceType.InformativeArmy, 0);
            Mechanism = new ResourceStack(GameResourceType.MechanismArmy, 0);
            Consumption = 0;
        }
        public void AddBattle(ResourceStack battle)
        {
            if(battle.Type == GameResourceType.BattleArmy)
            {
                Battle.Add(battle);
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
                Mechanism.Add(mechanism);
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
                Informative.Add(informative);
            }
            else
            {
                throw new Exception("Wrong type");
            }
        }
        public void DecreaseBattle(int count)
        {
            int decreasing = Math.Min(Battle.Count, count);
            Consumption += decreasing * battleArmyInfo.Worth;
            Battle.Split(decreasing);
        }
        public void DecreaseMechanism(int count)
        {
            int decreasing = Math.Min(Mechanism.Count, count);
            Consumption += decreasing * mechanismArmyInfo.Worth;
            Mechanism.Split(decreasing);
        }
        public void DecreaseInformative(int count)
        {
            int decreasing = Math.Min(Informative.Count, count);
            Consumption += decreasing * informativeArmyInfo.Worth;
            Informative.Split(decreasing);
        }
        public void AddConsumption(int count)
        {
            Consumption += count;
        }

        public int CalculateMechaAttack()
        {
            int totalMechaAttack = 0;
            totalMechaAttack += Mechanism.Count * mechanismArmyInfo.MechanAttack;
            totalMechaAttack += Informative.Count * informativeArmyInfo.MechanAttack;
            totalMechaAttack += Battle.Count * battleArmyInfo.MechanAttack;
            return totalMechaAttack;
        }
        public int CalculateMechaDefense()
        {
            int totalMechaDefense = 0;
            totalMechaDefense += Mechanism.Count * mechanismArmyInfo.MechanDefense;
            totalMechaDefense += Informative.Count * informativeArmyInfo.MechanDefense;
            totalMechaDefense += Battle.Count * battleArmyInfo.MechanDefense;
            return totalMechaDefense;
        }
        public int CalculateBattleAttack()
        {
            int totalBattleAttack = 0;
            totalBattleAttack += Mechanism.Count * mechanismArmyInfo.BattleAttack;
            totalBattleAttack += Informative.Count * informativeArmyInfo.BattleAttack;
            totalBattleAttack += Battle.Count * battleArmyInfo.BattleAttack;
            return totalBattleAttack;
        }

    }
}
