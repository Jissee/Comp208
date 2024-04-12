using EoE.GovernanceSystem.Interface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.GovernanceSystem.SrverInterface
{
    public interface IServerResourceList: IResourceList
    {
        ResourceListRecord GetResourceListRecord();
    }
}
