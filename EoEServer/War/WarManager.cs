﻿using EoE.Network.Entities;
using EoE.War;
using EoE.War.Interface;

namespace EoE.Server.War
{
    public class WarManager : IWarManager, ITickable
    {
        public Dictionary<string, IWar> WarDict { get; private set; } = new Dictionary<string, IWar>();
        public Dictionary<string, IWar> PreparingWarDict { get; private set; } = new Dictionary<string, IWar>();
        public Dictionary<IPlayer, Dictionary<IPlayer, WarTarget>> WarTargets { get; private set; }

        private List<string> removal = new List<string>();
        public IServer Server { get; }
        public WarManager(IServer server)
        {
            this.Server = server;
            this.WarTargets = new Dictionary<IPlayer, Dictionary<IPlayer, WarTarget>>();
        }
        public void PrepareNewWar(string name)
        {
            if (PreparingWarDict.ContainsKey(name))
            {
                PreparingWarDict.Remove(name);
            }
            IWarParty attackers = new WarParty();
            IWarParty defenders = new WarParty();
            IWar newWar = new War(attackers, defenders, name, Server, this);
            PreparingWarDict.Add(name, newWar);
        }
        public void DeclareWar(string warName)
        {
            IWar war = PreparingWarDict[warName];
            if (war != null)
            {
                WarDict.Add(war.WarName, war);
                PreparingWarDict.Remove(warName);
                //war.SetWarManager(this);
                WarTarget attackersTarget = new WarTarget();
                WarTarget defendersTarget = new WarTarget();
                foreach (IPlayer attackerPlayer in war.Attackers.Armies.Keys)
                {
                    foreach (IPlayer defenderPlayer in war.Defenders.Armies.Keys)
                    {
                        attackersTarget.SiliconClaim += WarTargets[attackerPlayer][defenderPlayer].SiliconClaim;
                        attackersTarget.CopperClaim += WarTargets[attackerPlayer][defenderPlayer].CopperClaim;
                        attackersTarget.IronClaim += WarTargets[attackerPlayer][defenderPlayer].IronClaim;
                        attackersTarget.AluminumClaim += WarTargets[attackerPlayer][defenderPlayer].AluminumClaim;
                        attackersTarget.ElectronicClaim += WarTargets[attackerPlayer][defenderPlayer].ElectronicClaim;
                        attackersTarget.IndustrialClaim += WarTargets[attackerPlayer][defenderPlayer].IndustrialClaim;
                        attackersTarget.FieldClaim += WarTargets[attackerPlayer][defenderPlayer].FieldClaim;
                        attackersTarget.PopClaim += WarTargets[attackerPlayer][defenderPlayer].PopClaim;

                        defendersTarget.SiliconClaim += WarTargets[defenderPlayer][attackerPlayer].SiliconClaim;
                        defendersTarget.CopperClaim += WarTargets[defenderPlayer][attackerPlayer].CopperClaim;
                        defendersTarget.IronClaim += WarTargets[defenderPlayer][attackerPlayer].IronClaim;
                        defendersTarget.AluminumClaim += WarTargets[defenderPlayer][attackerPlayer].AluminumClaim;
                        defendersTarget.ElectronicClaim += WarTargets[defenderPlayer][attackerPlayer].ElectronicClaim;
                        defendersTarget.IndustrialClaim += WarTargets[defenderPlayer][attackerPlayer].IndustrialClaim;
                        defendersTarget.FieldClaim += WarTargets[defenderPlayer][attackerPlayer].FieldClaim;
                        defendersTarget.PopClaim += WarTargets[defenderPlayer][attackerPlayer].PopClaim;
                    }
                }
                war.SetAttackersWarTarget(attackersTarget);
                war.SetDefendersWarTarget(defendersTarget);
            }
        }
        public void RemoveWar(IWar war)
        {
            if (WarDict.ContainsValue(war))
            {
                removal.Add(war.WarName);
            }
        }
        public void PlayerLose(IPlayer player)
        {
            foreach (var kvp in WarDict)
            {
                IWar war = kvp.Value;
                IWarParty attackers = war.Attackers;
                IWarParty defenders = war.Defenders;
                if (attackers.Contains(player))
                {
                    attackers.PlayerLose(player);
                }
                if (defenders.Contains(player))
                {
                    defenders.PlayerLose(player);
                }
            }
        }
        public void Tick()
        {
            foreach (var kvp in WarDict)
            {
                var war = kvp.Value;
                war.Tick();
            }
            CheckEnd();
            if (removal.Count > 0)
            {
                foreach (string warname in removal)
                {
                    WarDict.Remove(warname);
                }
                removal.Clear();
            }
            PreparingWarDict.Clear();

        }
        public void CheckEnd()
        {
            foreach (IWar checkWar in WarDict.Values)
            {
                if (checkWar.Attackers.AllSurrendered)
                {
                    if (checkWar.Defenders.AllSurrendered)
                    {
                        checkWar.End(null);
                    }
                    else
                    {
                        checkWar.End(checkWar.Attackers);
                    }
                }
                else if (checkWar.Defenders.AllSurrendered)
                {
                    checkWar.End(checkWar.Defenders);
                }
            }
        }
    }
}
