using EoE.GovernanceSystem;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem.ClientInterface;


namespace EoE.Network.Packets.GonverancePacket
{
    public class ResourceUpdatePacket : IPacket<ResourceUpdatePacket>
    {
        private ResourceListRecord resourceList;

        public ResourceUpdatePacket(ResourceListRecord resourceList)
        {
            this.resourceList = resourceList;
        }
        public static ResourceUpdatePacket Decode(BinaryReader reader)
        {
            return new ResourceUpdatePacket(ResourceListRecord.decoder(reader));
        }

        public static void Encode(ResourceUpdatePacket obj, BinaryWriter writer)
        {
            ResourceListRecord.encoder(obj.resourceList, writer);
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {
                INetworkEntity ne = context.Receiver!;
                if (ne is IClient client)
                {
                    if( client.GonveranceManager.ResourceList is IClientResourceList clientResourceList)
                    {
                        clientResourceList.Synchronize(resourceList);
                    }
                }
            }
        }
    }
}
