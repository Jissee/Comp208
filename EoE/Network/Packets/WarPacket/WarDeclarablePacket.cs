using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarDeclarablePacket : IPacket<WarDeclarablePacket>
    {
        public string[] names;
        public WarDeclarablePacket(string[] names) 
        {
            this.names = names;
        }
        public static WarDeclarablePacket Decode(BinaryReader reader)
        {
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for(int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarDeclarablePacket(names);
        }

        public static void Encode(WarDeclarablePacket obj, BinaryWriter writer)
        {
            writer.Write(obj.names.Length);
            for(int i = 0;i < obj.names.Length; i++)
            {
                writer.Write(obj.names[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IServerPlayerList playerList = server.PlayerList;
                List<IPlayer> list = playerList.TreatyManager.FindNonTruce(context.PlayerSender!);
                var namesEnum = from player in list
                                select player.PlayerName;
                WarDeclarablePacket packet = new WarDeclarablePacket(namesEnum.ToArray());
            }
            else
            {
                //todo show declareable players' name
            }
        }
    }
}
