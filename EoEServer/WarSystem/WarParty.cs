using EoE.GovernanceSystem;
using EoE.Network.Packets.WarPacket;
using EoE.Server.GovernanceSystem;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class WarParty : IWarParty
    {
        public Dictionary<IPlayer, IArmy> Armies { get; init; }
        private List<IPlayer> surrendered;
        public IArmy TotalArmy { get; private set; }
        private List<IPlayer> BattleArmyOwner;
        private List<IPlayer> InformativeArmyOwner;
        private List<IPlayer> MechanismArmyOwner;
        private IWar war;
        public int WarWidth => 60 / Math.Max(Armies.Count - surrendered.Count, 1);
        public bool AllSurrendered => surrendered.Count == Armies.Count;
        public WarParty() 
        { 
            Armies = new Dictionary<IPlayer,IArmy>();
            surrendered = new List<IPlayer>();
        }
        public void SetWar(IWar war)
        {
            this.war = war;
        }
        public bool Contains(IPlayer player)
        {
            return Armies.ContainsKey(player);
        }
        public void AddPlayer(IPlayer player)
        {
            Army army = new Army(this);
            Armies.Add(player, army);
        }
        public void PlayerSurrender(IPlayer player)
        {
            surrendered.Add(player);
            if(surrendered.Count == Armies.Count)
            {
                war.End(this);
            }
        }
        public void PlayerLose(IPlayer player)
        {
            if(player.IsLose && !surrendered.Contains(player))
            {
                Armies.Remove(player);
                return;
            }
            if (player.IsLose && surrendered.Contains(player))
            {
                Armies.Remove(player);
                surrendered.Remove(player);
            }
        }

        public void Dismiss()
        {
            foreach(var kvp in Armies)
            {
                IPlayer player = kvp.Key;
                IArmy army = kvp.Value;
                player.GonveranceManager.ResourceList.AddResourceStack(army.Battle);
                player.GonveranceManager.ResourceList.AddResourceStack(army.Informative);
                player.GonveranceManager.ResourceList.AddResourceStack(army.Mechanism);

                army.Battle.Split(army.Battle.Count);
                army.Informative.Split(army.Informative.Count);
                army.Mechanism.Split(army.Mechanism.Count);
            }
        }
        public void UpdataTotalArmy()
        {
            TotalArmy = new Army(this);
            foreach(var kvp in Armies)
            {
                IPlayer player = kvp.Key;
                IArmy army = kvp.Value;
                TotalArmy.AddBattle(army.Battle);
                TotalArmy.AddInformative(army.Informative);
                TotalArmy.AddMechanism(army.Mechanism);
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
                TotalArmy.AddConsumption(army.Consumption);
            }
        }
        public (int, int) GetMechAttackBattAttack()
        {
            return (TotalArmy.CalculateMechaAttack(), TotalArmy.CalculateBattleAttack());
        }

        public void AbsorbAttack(int MechAttack, int BattAttack)
        {
            UpdataTotalArmy();
            int remainingMechAttack = MechAttack - TotalArmy.CalculateMechaDefense();
            remainingMechAttack = Math.Max(remainingMechAttack, 0);
            int totalDamage = remainingMechAttack * 10 + BattAttack;

            int battleDamage = (int)(totalDamage * 0.68);
            int informativeDamage = (int)(totalDamage * 0.21);
            int mechanismDamage = (int)(totalDamage * 0.11);

            int battleTopDamage = TotalArmy.Battle.Count * 2;
            int informativeTopDamage = TotalArmy.Informative.Count * 2;
            int mechanismTopDamage = TotalArmy.Informative.Count * 2;

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
                IPlayer injuredPlayer = BattleArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseBattle(1);
                BattleArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= informativeDie; i++)
            {
                int dieIndex = rand.Next(InformativeArmyOwner.Count);
                IPlayer injuredPlayer = InformativeArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseInformative(1);
                InformativeArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= mechanismDie; i++)
            {
                int dieIndex = rand.Next(MechanismArmyOwner.Count);
                IPlayer injuredPlayer = MechanismArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseMechanism(1);
                MechanismArmyOwner.RemoveAt(dieIndex);
            }
        }
        public bool HasFilled(IPlayer player)
        {
            IArmy army = Armies[player];
            if(army.Battle.Count + army.Informative.Count + army.Mechanism.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FillInFrontier(IPlayer player, int battle, int informative, int mechanism)
        {
            Armies[player].Battle = new ResourceStack(GameResourceType.BattleArmy, battle);
            Armies[player].Informative = new ResourceStack(GameResourceType.InformativeArmy, informative);
            Armies[player].Mechanism = new ResourceStack(GameResourceType.MechanismArmy, mechanism);
        }
    }
}
