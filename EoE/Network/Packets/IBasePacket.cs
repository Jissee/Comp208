﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    public interface IBasePacket
    {
        void Handle(PacketContext context);
    }
}
