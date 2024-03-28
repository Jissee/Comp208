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
        public BattleArmyStack Battle { get; set; }
        public InformativeArmyStack Informative { get; set; }
        public MechanismArmyStack Mechanism { get; set; }
        public int Comsumption { get; private set; }
        public Army(WarParty warParty) 
        {
            this.warParty = warParty;
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
