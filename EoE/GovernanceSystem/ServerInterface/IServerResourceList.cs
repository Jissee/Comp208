using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.ServerInterface
{
    public interface IServerResourceList : IResourceList
    {
        void ClearAll();
    }
}
