using EoE.Network.Entities;
using EoE.Treaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.TreatyPacket
{
    public class QueryTreatyPacket : IPacket<QueryTreatyPacket>
    {
        private string[] names;
        public QueryTreatyPacket(string[] names) 
        {
            this.names = names;
        }
        public static QueryTreatyPacket Decode(BinaryReader reader)
        {
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for(int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new QueryTreatyPacket(names);
        }

        public static void Encode(QueryTreatyPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.names.Length);
            for(int i = 0;i < obj.names.Length; i++)
            {
                writer.Write(obj.names[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == Entities.NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                List<IPlayer> list = new List<IPlayer>();
                foreach (ITreaty treaty in server.PlayerList.TreatyManager.RelationTreatyList)
                {
                    if (treaty.FirstParty == player)
                    {
                        list.Add(treaty.SecondParty);
                    }
                    if(treaty.SecondParty == player)
                    {
                        list.Add(treaty.FirstParty);
                    }
                }
                var namesEnum = from target in list
                                select target.PlayerName;
                QueryTreatyPacket packet = new QueryTreatyPacket(namesEnum.ToArray());
                player.SendPacket(packet);
            }
            else
            {
                IClient client = (IClient) context.Receiver;
                client.ClientTreatyList.ChangeTreatyList(names);
            }
        }
    }
}
