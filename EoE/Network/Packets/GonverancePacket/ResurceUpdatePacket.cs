using EoE.GovernanceSystem;
using EoE.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace EoE.Network.Packets.GonverancePacket
{
    public class ResurceUpdatePacket : IPacket<ResurceUpdatePacket>
    {
        private IPlayerResourceList resourceList;

        public ResurceUpdatePacket(IPlayerResourceList resourceList)
        {
            this.resourceList = resourceList;
        }
        public static ResurceUpdatePacket Decode(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public static void Encode(ResurceUpdatePacket obj, BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Handle(PacketContext context)
        {
            throw new NotImplementedException();
        }
    }
}
