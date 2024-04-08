﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server.Treaty
{
    public class Treaty
    {
        public ServerPlayer FirstParty { get; init; }
        public ServerPlayer SecondParty { get; init; }

        public Treaty(ServerPlayer firstParty, ServerPlayer secondParty)
        {
            FirstParty = firstParty;
            SecondParty = secondParty;
        }
    }
}
