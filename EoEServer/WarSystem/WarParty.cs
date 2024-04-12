using EoE.Server.GovernanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarParty
    {
        public Dictionary<ServerPlayer, Army> Armies { get; init; }
        private List<ServerPlayer> surrendered;
        private Army totalArmy;
        private List<ServerPlayer> BattleArmyOwner;
        private List<ServerPlayer> InformativeArmyOwner;
        private List<ServerPlayer> MechanismArmyOwner;
        public int WarWidth => 60 / Math.Max(Armies.Count - surrendered.Count, 1);
        public bool AllSurrendered => surrendered.Count == Armies.Count;
        //public int Count => Armies.Count;
        public WarParty() 
        { 
            Armies = new Dictionary<ServerPlayer,Army>();
            surrendered = new List<ServerPlayer>();
        }

        public void AddPlayer(ServerPlayer player)
        {
            Army army = new Army(this);
            Armies.Add(player, army);
        }
        public void PlayerSurrender(ServerPlayer player)
        {
            surrendered.Add(player);
        }

        public void Dismiss()
        {
            foreach(var kvp in Armies)
            {
                ServerPlayer player = kvp.Key;
                Army army = kvp.Value;
                player.GonveranceManager.ResourceList.AddResourceStack(army.Battle);
                player.GonveranceManager.ResourceList.AddResourceStack(army.Informative);
                player.GonveranceManager.ResourceList.AddResourceStack(army.Mechanism);
            }
        }
        public void UpdataTotalArmy()
        {
            totalArmy = new Army(this);
            foreach(var kvp in Armies)
            {
                ServerPlayer player = kvp.Key;
                Army army = kvp.Value;
                totalArmy.AddBattle(army.Battle);
                totalArmy.AddInformative(army.Informative);
                totalArmy.AddMechanism(army.Mechanism);
                for(int i = 1; i <= army.Battle.Count; i++)
                {
                    BattleArmyOwner.Add(player);
                }
                for (int i = 1; i <= army.Informative.Count; i++)
                {
                    InformativeArmyOwner.Add(player);
                }
                for (int i = 1; i <= army.Mechanism.Count; i++)
                {
                    MechanismArmyOwner.Add(player);
                }
                totalArmy.AddConsumption(army.Consumption);
            }
        }
        public (int, int) GetMechAttackBattAttack()
        {
            return (totalArmy.CalculateMechaAttack(), totalArmy.CalculateBattleAttack());
        }

        public void AbsorbAttack(int MechAttack, int BattAttack)
        {
            UpdataTotalArmy();
            int remainingMechAttack = MechAttack - totalArmy.CalculateMechaDefense();
            remainingMechAttack = Math.Max(remainingMechAttack, 0);
            int totalDamage = remainingMechAttack * 10 + BattAttack;

            int battleDamage = (int)(totalDamage * 0.68);
            int informativeDamage = (int)(totalDamage * 0.21);
            int mechanismDamage = (int)(totalDamage * 0.11);

            int battleTopDamage = totalArmy.Battle.Count * 2;
            int informativeTopDamage = totalArmy.Informative.Count * 2;
            int mechanismTopDamage = totalArmy.Informative.Count * 2;

            if(battleDamage > battleTopDamage)
            {
                informativeDamage += battleTopDamage - battleDamage;
                battleDamage = battleTopDamage;
            }
            if (informativeDamage > informativeTopDamage)
            {
                mechanismDamage += informativeTopDamage - informativeDamage;
                informativeDamage = informativeTopDamage;
            }
            if(mechanismDamage > mechanismTopDamage)
            {
                mechanismDamage = mechanismTopDamage;
            }

            int battleDie = battleDamage / 2;
            int informativeDie = informativeDamage / 2;
            int mechanismDie = mechanismDamage / 2;
            Random rand = new Random();
            for(int i = 1; i <= battleDie; i++)
            {
                int dieIndex = rand.Next(BattleArmyOwner.Count);
                ServerPlayer injuredPlayer = BattleArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseBattle(1);
                BattleArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= informativeDie; i++)
            {
                int dieIndex = rand.Next(InformativeArmyOwner.Count);
                ServerPlayer injuredPlayer = InformativeArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseInformative(1);
                InformativeArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= mechanismDie; i++)
            {
                int dieIndex = rand.Next(MechanismArmyOwner.Count);
                ServerPlayer injuredPlayer = MechanismArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseMechanism(1);
                MechanismArmyOwner.RemoveAt(dieIndex);
            }
        }
    }
}
