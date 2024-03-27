using EoE.GovernanceSystem;
using EoE.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    public class ResurceUpdatePacket : IPacket<ResurceUpdatePacket>
    {
        private Dictionary<GameResourceType, int> resContain;
        
        public ResurceUpdatePacket(Dictionary<GameResourceType, int> newResContain)
        {
            this.resContain = newResContain;
        }
        public static ResurceUpdatePacket Decode(BinaryReader reader)
        {
            Dictionary<GameResourceType, int> newResContain = new Dictionary<GameResourceType, int>();
            for (int i = 0; i < 5; i++)
            {
                string keyString = reader.ReadString();
                int value = reader.ReadInt32();
                GameResourceType key = Enum.Parse<GameResourceType>(keyString);
                newResContain.Add(key,value);
            }
            ResurceUpdatePacket packet = new ResurceUpdatePacket(newResContain);
            return packet;
        }

        public static void Encode(ResurceUpdatePacket obj, BinaryWriter writer)
        {

            foreach (var item in obj.resContain)
            {
                writer.Write(item.Key.ToString());
                writer.Write(item.Value);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Server2Client)
            {

            }
        }
    }
}
