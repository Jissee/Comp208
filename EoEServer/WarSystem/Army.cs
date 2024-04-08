using EoE.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class Army
    {
        private WarParty warParty;
        public BattleArmyStack Battle { get; init; }
        public InformativeArmyStack Informative { get; init; }
        public MechanismArmyStack Mechanism { get; init; }
        public int Consumption { get; private set; }
        public Army(WarParty warParty) 
        {
            this.warParty = warParty;
            Battle = new BattleArmyStack(0);
            Informative = new InformativeArmyStack(0);
            Mechanism = new MechanismArmyStack(0);
            Consumption = 0;
        }
        public void AddBattle(BattleArmyStack battle)
        {
            Battle.Add(battle);
        }
        public void AddMechanism(MechanismArmyStack mechanism)
        {
            Mechanism.Add(mechanism);
        }

        public void AddInformative(InformativeArmyStack informative)
        {
            Informative.Add(informative);
        }
        public void DecreaseBattle(int count)
        {
            int decreasing = Math.Min(Battle.Count, count);
            Consumption += decreasing * Battle.Worth;
            Battle.Count -=  decreasing;
        }
        public void DecreaseMechanism(int count)
        {
            int decreasing = Math.Min(Mechanism.Count, count);
            Consumption += decreasing * Mechanism.Worth;
            Mechanism.Count -= decreasing;
        }
        public void DecreaseInformative(int count)
        {
            int decreasing = Math.Min(Informative.Count, count);
            Consumption += decreasing * Informative.Worth;
            Informative.Count -= decreasing;
        }
        public void AddConsumption(int count)
        {
            Consumption += count;
        }

        public int CalculateMechaAttack()
        {
            int totalMechaAttack = 0;
            totalMechaAttack += Mechanism.Count * Mechanism.MechanAttack;
            totalMechaAttack += Informative.Count * Informative.MechanAttack;
            totalMechaAttack += Battle.Count * Battle.MechanAttack;
            return totalMechaAttack;
        }
        public int CalculateMechaDefense()
        {
            int totalMechaDefense = 0;
            totalMechaDefense += Mechanism.Count * Mechanism.MechanDefense;
            totalMechaDefense += Informative.Count * Informative.MechanDefense;
            totalMechaDefense += Battle.Count * Battle.MechanDefense;
            return totalMechaDefense;
        }
        public int CalculateBattleAttack()
        {
            int totalBattleAttack = 0;
            totalBattleAttack += Mechanism.Count * Mechanism.BattleAttack;
            totalBattleAttack += Informative.Count * Informative.BattleAttack;
            totalBattleAttack += Battle.Count * Battle.BattleAttack;
            return totalBattleAttack;
        }

    }
}
