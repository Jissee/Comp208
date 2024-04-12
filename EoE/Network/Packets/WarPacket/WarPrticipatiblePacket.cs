using EoE.Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarPrticipatiblePacket : IPacket<WarPrticipatiblePacket>
    {
        private string target;
        private string[] names;
        private WarPrticipatiblePacket(string target, string[] names)
        {
            this.target = target;
            this.names = names;
        }
        public WarPrticipatiblePacket(string target) : this(target, []) {}
        public WarPrticipatiblePacket(string[] names) : this("", names) { }

        public static WarPrticipatiblePacket Decode(BinaryReader reader)
        {
            string target = reader.ReadString();
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarPrticipatiblePacket(target, names);
        }

        public static void Encode(WarPrticipatiblePacket obj, BinaryWriter writer)
        {
            writer.Write(obj.target);
            writer.Write(obj.names.Length);
            for (int i = 0; i < obj.names.Length; i++)
            {
                writer.Write(obj.names[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer targetPlayer = server.GetPlayer(target)!;
                List<IPlayer> protectors = server.GetProtectorsRecursively(targetPlayer);
                List<IPlayer> allPlayers = [.. server.PlayerList.Players];
                allPlayers.RemoveAll(protectors.Contains);

                var nameEnum = from participatable in allPlayers
                               select participatable.PlayerName;

                WarPrticipatiblePacket packet = new WarPrticipatiblePacket(nameEnum.ToArray());
                context.PlayerSender!.SendPacket(packet);
            }
            else
            {
                //todo players select their target
            }
        }
    }
}
