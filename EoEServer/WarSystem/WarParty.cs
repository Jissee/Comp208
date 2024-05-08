using EoE.GovernanceSystem;
using EoE.Network.Packets.GonverancePacket;
using EoE.WarSystem.Interface;

namespace EoE.Server.WarSystem
{
    public class WarParty : IWarParty
    {
        public Dictionary<IPlayer, IArmy> Armies { get; init; }
        private List<IPlayer> surrendered;
        public IArmy? TotalArmy { get; private set; }
        private List<IPlayer> battleArmyOwner;
        private List<IPlayer> informativeArmyOwner;
        private List<IPlayer> mechanismArmyOwner;
        private IWar? war;
        public int WarWidth => 60 / Math.Max(Armies.Count - surrendered.Count, 1);
        public bool AllSurrendered => surrendered.Count == Armies.Count;
        public WarParty()
        {
            Armies = new Dictionary<IPlayer, IArmy>();
            surrendered = new List<IPlayer>();
            battleArmyOwner = new List<IPlayer>();
            informativeArmyOwner = new List<IPlayer>();
            mechanismArmyOwner = new List<IPlayer>();
        }
        public void Clear()
        {
            Armies.Clear();
            surrendered.Clear();
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
        }
        public void PlayerLose(IPlayer player)
        {
            if (player.IsLose && !surrendered.Contains(player))
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
            foreach (var kvp in Armies)
            {
                IPlayer player = kvp.Key;
                IArmy army = kvp.Value;
                player.GonveranceManager.ResourceList.AddResource(GameResourceType.BattleArmy, army.GetBattleCount());
                player.GonveranceManager.ResourceList.AddResource(GameResourceType.InformativeArmy, army.GetInformativeCount());
                player.GonveranceManager.ResourceList.AddResource(GameResourceType.MechanismArmy, army.GetMechanismCount());

                army.Clear();
                player.SendPacket(new ResourceUpdatePacket(player.GonveranceManager.ResourceList.GetResourceListRecord()));
            }
        }
        public void UpdateTotalArmy()
        {
            battleArmyOwner = new List<IPlayer>();
            informativeArmyOwner = new List<IPlayer>();
            mechanismArmyOwner = new List<IPlayer>();
            TotalArmy = new Army(this);
            foreach (var kvp in Armies)
            {
                IPlayer player = kvp.Key;
                IArmy army = kvp.Value;
                TotalArmy.AddBattle(new ResourceStack(GameResourceType.BattleArmy, army.GetBattleCount()));
                TotalArmy.AddInformative(new ResourceStack(GameResourceType.InformativeArmy, army.GetInformativeCount()));
                TotalArmy.AddMechanism(new ResourceStack(GameResourceType.MechanismArmy, army.GetMechanismCount()));
                for (int i = 1; i <= army.GetBattleCount(); i++)
                {
                    battleArmyOwner.Add(player);
                }
                for (int i = 1; i <= army.GetInformativeCount(); i++)
                {
                    informativeArmyOwner.Add(player);
                }
                for (int i = 1; i <= army.GetMechanismCount(); i++)
                {
                    mechanismArmyOwner.Add(player);
                }
                TotalArmy.AddConsumption(army.Consumption);
            }
        }
        public (int, int) GetMechAttackBattAttack()
        {
            UpdateTotalArmy();
            return (TotalArmy!.CalculateMechaAttack(), TotalArmy.CalculateBattleAttack());
        }

        public void AbsorbAttack(int MechAttack, int BattAttack)
        {
            int remainingMechAttack = MechAttack - TotalArmy!.CalculateMechaDefense();
            remainingMechAttack = Math.Max(remainingMechAttack, 0);
            int totalDamage = remainingMechAttack * 6 + BattAttack;

            int battleDamage = (int)(totalDamage * 0.68);
            int informativeDamage = (int)(totalDamage * 0.21);
            int mechanismDamage = (int)(totalDamage * 0.11);

            int battleTopDamage = TotalArmy.GetBattleCount() * 2;
            int informativeTopDamage = TotalArmy.GetInformativeCount() * 2;
            int mechanismTopDamage = TotalArmy.GetMechanismCount() * 6;

            if (battleDamage > battleTopDamage)
            {
                informativeDamage += battleDamage - battleTopDamage;
                battleDamage = battleTopDamage;
            }
            if (informativeDamage > informativeTopDamage)
            {
                mechanismDamage += informativeDamage - informativeTopDamage;
                informativeDamage = informativeTopDamage;
            }
            if (mechanismDamage > mechanismTopDamage)
            {
                mechanismDamage = mechanismTopDamage;
            }

            int battleDie = battleDamage / 2;
            int informativeDie = informativeDamage / 2;
            int mechanismDie = mechanismDamage / 6;
            Random rand = new Random();
            for (int i = 1; i <= battleDie; i++)
            {
                int dieIndex = rand.Next(battleArmyOwner.Count);
                IPlayer injuredPlayer = battleArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseBattle(1);
                battleArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= informativeDie; i++)
            {
                int dieIndex = rand.Next(informativeArmyOwner.Count);
                IPlayer injuredPlayer = informativeArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseInformative(1);
                informativeArmyOwner.RemoveAt(dieIndex);
            }
            for (int i = 1; i <= mechanismDie; i++)
            {
                int dieIndex = rand.Next(mechanismArmyOwner.Count);
                IPlayer injuredPlayer = mechanismArmyOwner[dieIndex];
                Armies[injuredPlayer].DecreaseMechanism(1);
                mechanismArmyOwner.RemoveAt(dieIndex);
            }
            UpdateTotalArmy();
            Dismiss();
        }
        public bool HasFilled(IPlayer player)
        {
            IArmy army = Armies[player];
            if (army.GetBattleCount() + army.GetInformativeCount() + army.GetMechanismCount() > 0)
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
            Armies[player].AddBattle(new ResourceStack(GameResourceType.BattleArmy, battle));
            Armies[player].AddInformative(new ResourceStack(GameResourceType.InformativeArmy, informative));
            Armies[player].AddMechanism(new ResourceStack(GameResourceType.MechanismArmy, mechanism));
        }
    }
}
