using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarIntensionPacket : IPacket<WarIntensionPacket>
    {
        private string targetPlayerName;
        private List<string> protectors;
        public WarIntensionPacket(string targetPlayerName, string[] protectors)
        {
            this.targetPlayerName = targetPlayerName;
            this.protectors = [.. protectors];
        }
        public static WarIntensionPacket Decode(BinaryReader reader)
        {
            string targetPlayerName = reader.ReadString();
            int protectorCount = reader.ReadInt32();
            List<string> protectors = new List<string>();
            for (int i = 0; i < protectorCount; i++)
            {
                protectors.Add(reader.ReadString());
            }
            return new WarIntensionPacket(targetPlayerName, [.. protectors]);
        }

        public static void Encode(WarIntensionPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.targetPlayerName);
            writer.Write(obj.protectors.Count);
            foreach(string protector in obj.protectors)
            {
                writer.Write(protector);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer targetPlayer = server.GetPlayer(targetPlayerName)!;
                List<IPlayer> protectors = server.GetProtectorsRecursively(targetPlayer);
                var nameEnum = from protector in protectors
                               select protector.PlayerName;

                WarIntensionPacket packet = new WarIntensionPacket(targetPlayerName, nameEnum.ToArray());
                context.PlayerSender!.SendPacket(packet);
            }
            else
            {
                //todo: client implementation
            }
        }
    }
}
