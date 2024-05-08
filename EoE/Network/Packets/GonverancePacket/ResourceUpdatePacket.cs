using EoE.Governance.ClientInterface;
using EoE.Network.Entities;
using EoE.Network.Packets.GonverancePacket.Record;


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
                    if (client.GonveranceManager.ResourceList is IClientResourceList clientResourceList)
                    {
                        clientResourceList.Synchronize(resourceList);
                    }
                }
            }
        }
    }
}
