﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IWarParty
    {
        int WarWidth { get; }
        Dictionary<IPlayer, IArmy> Armies { get; }
        IArmy TotalArmy { get; }
        void SetWar(IWar war);
        void PlayerSurrender(IPlayer player);
        void PlayerLose(IPlayer player);
        bool Contains(IPlayer player);
        void FillInFrontier(IPlayer player, int battle, int informative, int mechanism);
        (int, int) GetMechAttackBattAttack();
        bool HasFilled(IPlayer player);
    }
}
