﻿using EoE.Governance;

namespace EoE.War.Interface
{
    /// <summary>
    /// Indicating the army, consisting of three different types of the soldiers.
    /// </summary>
    public interface IArmy
    {
        int Consumption { get; }
        void AddBattle(ResourceStack battle);
        void AddMechanism(ResourceStack mechanism);
        void AddInformative(ResourceStack informative);
        int GetBattleCount();
        int GetInformativeCount();
        int GetMechanismCount();
        void AddConsumption(int count);
        void Clear();
        void DecreaseBattle(int count);
        void DecreaseMechanism(int count);
        void DecreaseInformative(int count);

        int CalculateMechaAttack();
        int CalculateMechaDefense();
        int CalculateBattleAttack();
    }
}