﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    internal class CommonDefenseTreaty : Treaty
    {
        public CommonDefenseTreaty(ServerPlayer firstParty, ServerPlayer secondParty) : base(firstParty, secondParty)
        {
        }

        public override void Tick()
        {
        }
    }
}
