using EoE.GovernanceSystem;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.WarPacket;
using EoE.Server.Treaty;
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
        public WarTarget AttackersTarget { get; private set; }
        public WarTarget DefendersTarget { get; private set; }
        public IWarManager WarManager { get; private set; }
        private bool status = true;
        private IServer Server;

        public War(IWarParty attackers, IWarParty defenders, string warName, IServer server)
        {
            this.Attackers = attackers;
            this.Defenders = defenders;
            WarName = warName;
            attackers.SetWar(this);
            defenders.SetWar(this);
            this.Server = server;
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

        public IWarParty GetWarEnemyPartyOfPlayer(IPlayer player)
        {
            if (Attackers.Contains(player))
            {
                return Defenders;
            }
            else if (Defenders.Contains(player))
            {
                return Attackers;
            }
            throw new Exception("No playerWinner in this war");
        }
        public void SetAttackersWarTarget(WarTarget warTarget)
        {
            AttackersTarget = warTarget;
        }
        public void SetDefendersWarTarget(WarTarget warTarget) 
        {
            DefendersTarget = warTarget;
        }
        public void End(IWarParty? defeated)
        {
            foreach(var kvpFirst in Attackers.Armies)
            {
                IPlayer playerFirst = kvpFirst.Key;
                foreach(var kvpSecond in Defenders.Armies)
                {
                    IPlayer playerSecond = kvpSecond.Key;
                    Server.PlayerList.TreatyManager.AddTruceTreaty(playerFirst, playerSecond);
                }
            }
            if(defeated != null)
            {
                if (defeated == Attackers)
                {
                    DivideSpoil(Defenders, Attackers, DefendersTarget);
                }
                else
                {
                    DivideSpoil(Attackers, Defenders, AttackersTarget);
                }
            }
            status = false;
            WarManager.RemoveWar(this);
        }
        private void DivideSpoil(IWarParty winner, IWarParty loser, WarTarget winnerTarget)
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
                    record.siliconCount = (int)(winnerTarget.SiliconClaim * winnerProportion * loserProportion);
                    record.copperCount = (int)(winnerTarget.CopperClaim * winnerProportion * loserProportion);
                    record.ironCount = (int)(winnerTarget.IronClaim * winnerProportion * loserProportion);
                    record.aluminumCount = (int)(winnerTarget.AluminumClaim * winnerProportion * loserProportion);
                    record.electronicCount = (int)(winnerTarget.ElectronicClaim * winnerProportion * loserProportion);
                    record.industrialCount = (int)(winnerTarget.IndustrialClaim * winnerProportion * loserProportion);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Silicon, record.siliconCount);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Copper, record.copperCount);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Iron, record.ironCount);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Aluminum, record.aluminumCount);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Electronic, record.electronicCount);
                    playerWinner.GonveranceManager.ResourceList.AddResource(GameResourceType.Industrial, record.industrialCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Silicon, record.siliconCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Copper, record.copperCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Iron, record.ironCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Aluminum, record.aluminumCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Electronic, record.electronicCount);
                    playerLoser.GonveranceManager.ResourceList.SplitResource(GameResourceType.Industrial, record.industrialCount);

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
        private void SurrenderAnouncement(string surrenderName, IWarParty attackerParty, IWarParty defenderParty)
        {
            ServerMessagePacket packet = new ServerMessagePacket($"{surrenderName} surrenders in war {WarName}!");
            foreach(IPlayer player in attackerParty.Armies.Keys)
            {
                player.SendPacket(packet);
            }
            foreach (IPlayer player in defenderParty.Armies.Keys)
            {
                player.SendPacket(packet);
            }
        }
        private void AutoSurrender()
        {
            foreach (var kvp in Attackers.Armies)
            {
                IPlayer player = kvp.Key;
                if (!Attackers.HasFilled(player))
                {
                    SurrenderAnouncement(player.PlayerName, Attackers, Defenders);
                    Attackers.PlayerSurrender(player);
                }
            }
            foreach (var kvp in Defenders.Armies)
            {
                IPlayer player = kvp.Key;
                if (!Defenders.HasFilled(player))
                {
                    SurrenderAnouncement(player.PlayerName, Attackers, Defenders);
                    Defenders.PlayerSurrender(player);
                }
            }
        }
        private void WarTick()
        {

            (int, int) attackerAttack = Attackers.GetMechAttackBattAttack();
            (int, int) defenderAttack = Defenders.GetMechAttackBattAttack();

            int aB1 = Attackers.TotalArmy.GetBattleCount();
            int aI1 = Attackers.TotalArmy.GetInformativeCount();
            int aM1 = Attackers.TotalArmy.GetMechanismCount();

            int dB1 = Defenders.TotalArmy.GetBattleCount();
            int dI1 = Defenders.TotalArmy.GetInformativeCount();
            int dM1 = Defenders.TotalArmy.GetMechanismCount();

            Attackers.AbsorbAttack(defenderAttack.Item1, defenderAttack.Item2);
            Defenders.AbsorbAttack(attackerAttack.Item1, attackerAttack.Item2);


            int aB2 = Attackers.TotalArmy.GetBattleCount();
            int aI2 = Attackers.TotalArmy.GetInformativeCount();
            int aM2 = Attackers.TotalArmy.GetMechanismCount();
                  
            int dB2 = Defenders.TotalArmy.GetBattleCount();
            int dI2 = Defenders.TotalArmy.GetInformativeCount();
            int dM2 = Defenders.TotalArmy.GetMechanismCount();

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
            WarManager.Server.Boardcast(packetA, Attackers.Contains);
            WarManager.Server.Boardcast(packetD, Defenders.Contains);
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
