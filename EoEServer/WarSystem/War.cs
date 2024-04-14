using EoE.GovernanceSystem;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.WarPacket;
using EoE.Util;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.WarSystem
{
    public class War : IWar
    {
        public string WarName { get; private set; }
        public IWarParty Attackers { get; private set; }
        public IWarParty Defenders { get; private set; }
        public IWarTarget AttackersTarget { get; private set; }
        public IWarTarget DefendersTarget { get; private set; }
        public IWarManager WarManager { get; private set; }
        private bool status = true;

        public War(IWarParty attackers, IWarParty defenders, string warName)
        {
            this.Attackers = attackers;
            this.Defenders = defenders;
            WarName = warName;
            attackers.SetWar(this);
            defenders.SetWar(this);
        }
        public void SetWarManager(IWarManager manager)
        {
            this.WarManager = manager;
        }
        public IWarParty GetWarPartyOfPlayer(IPlayer player)
        {
            if(Attackers.Contains(player))
            {
                return Attackers;
            }
            else if (Defenders.Contains(player))
            {
                return Defenders;
            }
            throw new Exception("No playerWinner in this war");
        }
        public void SetAttackersWarTarget(IWarTarget warTarget)
        {
            AttackersTarget = warTarget;
        }
        public void SetDefendersWarTarget(IWarTarget warTarget) 
        {
            DefendersTarget = warTarget;
        }
        public void End(IWarParty defeated)
        {
            
            if (defeated == Attackers)
            {
                DivideSpoil(Defenders, Attackers, DefendersTarget);
            }
            else
            {
                DivideSpoil(Attackers, Defenders, AttackersTarget);
            }
            status = false;
            WarManager.RemoveWar(this);
        }
        private int GetResourceClaim(IWarTarget target, GameResourceType type)
        {
            int claim = 0;
            foreach(var resource in target.ResourceClaim)
            {
                if(resource.Type == type)
                {
                    claim = resource.Count;
                    break;
                }
            }
            return claim;
        }
        private void DivideSpoil(IWarParty winner, IWarParty loser, IWarTarget winnerTarget)
        {
            int winnerTotalConsume = winner.TotalArmy.Consumption;
            int loserTotalConsume = loser.TotalArmy.Consumption;
            foreach(var kvpWinner in winner.Armies)
            {
                IPlayer playerWinner = kvpWinner.Key;
                IArmy armyWinner = kvpWinner.Value;
                int playerWinnerConsume = armyWinner.Consumption;
                double winnerProportion = (double) playerWinnerConsume / winnerTotalConsume;
                double loserTotalWeight = 0;
                foreach(var kvpLoser in loser.Armies)
                {
                    IPlayer playerLoser = kvpLoser.Key;
                    IArmy armyLoser = kvpLoser.Value;
                    int playerLoserConsume = armyLoser.Consumption;
                    loserTotalWeight += (double)loserTotalConsume / playerLoserConsume;
                }
                foreach(var kvpLoser in loser.Armies)
                {
                    IPlayer playerLoser = kvpLoser.Key;
                    IArmy armyLoser = kvpLoser.Value;
                    int playerLoserConsume = armyLoser.Consumption;
                    double loserProportion = ((double)loserTotalConsume / playerLoserConsume) / loserTotalWeight;
                    ResourceListRecord record = new ResourceListRecord();
                    record.siliconCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Silicon) * winnerProportion * loserProportion);
                    record.copperCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Copper) * winnerProportion * loserProportion);
                    record.ironCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Iron) * winnerProportion * loserProportion);
                    record.aluminumCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Aluminum) * winnerProportion * loserProportion);
                    record.electronicCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Electronic) * winnerProportion * loserProportion);
                    record.industrialCount = (int)(GetResourceClaim(winnerTarget, GameResourceType.Industrial) * winnerProportion * loserProportion);
                    int popCompensation = (int)(winnerTarget.PopClaim * winnerProportion * loserProportion);
                    int fieldCompensation = (int)(winnerTarget.FieldClaim * winnerProportion * loserProportion);
                    int actualPopCompensation = Math.Min(popCompensation, playerLoser.GonveranceManager.PopManager.TotalPopulation);
                    int actualFieldCompensation = Math.Min(fieldCompensation, playerLoser.GonveranceManager.FieldList.TotalFieldCount);
                    playerWinner.GonveranceManager.PopManager.AlterPop(actualPopCompensation);
                    playerLoser.GonveranceManager.PopManager.AlterPop(-actualPopCompensation);

                    ProbabilityList<GameResourceType> loserFieldLost = new ProbabilityList<GameResourceType> { };
                    FieldListRecord fieldRecord = playerLoser.GonveranceManager.FieldList.GetFieldListRecord();
                    loserFieldLost.Add(GameResourceType.Silicon, fieldRecord.siliconFieldCount);
                    loserFieldLost.Add(GameResourceType.Copper, fieldRecord.copperFieldCount);
                    loserFieldLost.Add(GameResourceType.Iron, fieldRecord.ironFieldCount);
                    loserFieldLost.Add(GameResourceType.Aluminum, fieldRecord.aluminumFieldCount);
                    loserFieldLost.Add(GameResourceType.Electronic, fieldRecord.electronicFieldCount);
                    loserFieldLost.Add(GameResourceType.Industrial, fieldRecord.industrialFieldCount);

                    for (int i = 0; i < actualFieldCompensation; i++)
                    {
                        GameResourceType type = loserFieldLost.GetAndDecreaseOne();
                        playerLoser.GonveranceManager.FieldList.SplitField(type, 1);
                        playerWinner.GonveranceManager.FieldList.AddField(type, 1);
                    }
                    WarCompensationInfoPacket packet = new WarCompensationInfoPacket(this.WarName, record, popCompensation, fieldCompensation, playerWinner.PlayerName);
                    playerWinner.SendPacket(packet);
                }
                
            }
        }
        private void AutoSurrender()
        {
            foreach (var kvp in Attackers.Armies)
            {
                IPlayer player = kvp.Key;
                if (!Attackers.HasFilled(player))
                {
                    Attackers.PlayerSurrender(player);
                }
            }
            foreach (var kvp in Defenders.Armies)
            {
                IPlayer player = kvp.Key;
                if (!Defenders.HasFilled(player))
                {
                    Defenders.PlayerSurrender(player);
                }
            }
        }
        private void WarTick()
        {

            (int, int) attackerAttack = Attackers.GetMechAttackBattAttack();
            (int, int) defenderAttack = Defenders.GetMechAttackBattAttack();

            int aB1 = Attackers.TotalArmy.Battle.Count;
            int aI1 = Attackers.TotalArmy.Informative.Count;
            int aM1 = Attackers.TotalArmy.Mechanism.Count;

            int dB1 = Defenders.TotalArmy.Battle.Count;
            int dI1 = Defenders.TotalArmy.Informative.Count;
            int dM1 = Defenders.TotalArmy.Mechanism.Count;

            Attackers.AbsorbAttack(defenderAttack.Item1, defenderAttack.Item2);
            Defenders.AbsorbAttack(attackerAttack.Item1, attackerAttack.Item2);


            int aB2 = Attackers.TotalArmy.Battle.Count;
            int aI2 = Attackers.TotalArmy.Informative.Count;
            int aM2 = Attackers.TotalArmy.Mechanism.Count;

            int dB2 = Defenders.TotalArmy.Battle.Count;
            int dI2 = Defenders.TotalArmy.Informative.Count;
            int dM2 = Defenders.TotalArmy.Mechanism.Count;

            WarInformationPacket packetA = new WarInformationPacket(
                WarName,
                aB1, aI1, aM1, aB1 - aB2, aI1 - aI2, aM1 - aM2,
                dB1, dI1, dM1, dB1 - dB2, dI1 - dI2, dM1 - dM2
                );
            WarInformationPacket packetD = new WarInformationPacket(
                WarName,
                dB1, dI1, dM1, dB1 - dB2, dI1 - dI2, dM1 - dM2,
                aB1, aI1, aM1, aB1 - aB2, aI1 - aI2, aM1 - aM2
                );
            WarManager.Server.Broadcast(packetA, Attackers.Contains);
            WarManager.Server.Broadcast(packetD, Defenders.Contains);
        }
        
        public void Tick()
        {
            AutoSurrender();
            if(status == true)
            {
                WarTick();
            }
        }

    }
}
