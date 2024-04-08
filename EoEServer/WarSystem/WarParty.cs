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
                player.GonveranceManager.ResourceList.AddResource(army.Battle);
                player.GonveranceManager.ResourceList.AddResource(army.Informative);
                player.GonveranceManager.ResourceList.AddResource(army.Mechanism);
            }
        }
        public void UpdataTotalArmy()
        {
            totalArmy = new Army(this);
            foreach(var kvp in Armies)
            {
                Army army = kvp.Value;
                totalArmy.AddBattle(army.Battle);
                totalArmy.AddInformative(army.Informative);
                totalArmy.AddMechanism(army.Mechanism);
                totalArmy.AddConsumption(army.Consumption);
            }
        }
        public (int, int) GetMechAttackBattAttack()
        {
            return (totalArmy.CalculateMechaAttack(), totalArmy.CalculateBattleAttack());
        }

        public void AbsorbAttack(int MechAttack, int BattAttack)
        {
            int remainingMechAttack = MechAttack - totalArmy.CalculateMechaDefense();
            remainingMechAttack = Math.Max(remainingMechAttack, 0);
            int totalDamage = remainingMechAttack * 10 + BattAttack;
            int battleDamage = (int)(totalDamage * 0.68 / 2);
            int informativeDamage = (int)(totalDamage * 0.21 / 2);
            int mechanismDamage = (int)(totalDamage * 0.11 / 2);
            foreach(var kvp in Armies)
            {
                Army army = kvp.Value;
                double battleRate = 0;
                double informativeRate = 0;
                double mechanismRate = 0;
                if (totalArmy.Battle.Count > 0)
                {
                    battleRate = (double)army.Battle.Count / totalArmy.Battle.Count;
                }
                if(totalArmy.Mechanism.Count > 0)
                {
                    mechanismRate = (double)army.Mechanism.Count / totalArmy.Mechanism.Count;
                }
                if(totalArmy.Informative.Count > 0)
                {
                    informativeRate = (double)army.Informative.Count / totalArmy.Informative.Count;
                }
                int playerBattleDamage = (int)(battleDamage * battleRate);
                int playerInformativeDamage = (int)(informativeDamage * informativeRate);
                int playerMechanismDamage = (int)(mechanismDamage * mechanismRate);
                army.DecreaseBattle(playerBattleDamage);
                army.DecreaseInformative(playerInformativeDamage);
                army.DecreaseMechanism(playerMechanismDamage);
            }
        }
    }
}
