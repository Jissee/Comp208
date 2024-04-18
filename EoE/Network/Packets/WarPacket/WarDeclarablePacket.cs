using EoE.Network.Entities;
using EoE.Treaty;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarDeclarablePacket : IPacket<WarDeclarablePacket>
    {
        private string warName;
        private string[] names;
        public WarDeclarablePacket(string warName, string[] names) 
        {
            this.warName = warName;
            this.names = names;
        }
        public static WarDeclarablePacket Decode(BinaryReader reader)
        {
            string warName = reader.ReadString();
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for(int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarDeclarablePacket(warName, names);
        }

        public static void Encode(WarDeclarablePacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warName);
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
                foreach(IWar theWar in server.PlayerList.WarManager.WarDict.Values)
                {
                    foreach(IPlayer player in theWar.Attackers.Armies.Keys)
                    {
                        if (list.Contains(player))
                        {
                            list.Remove(player);
                        }
                    }
                    foreach(IPlayer player in theWar.Defenders.Armies.Keys)
                    {
                        if (list.Contains(player))
                        {
                            list.Remove(player);
                        }
                    }
                }
                list.Remove(context.PlayerSender!);
                for(int i=0; i < list.Count; i++)
                {
                    if (server.PlayerList.GetProtectorsRecursively(list[i]).Contains(context.PlayerSender!))
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                var namesEnum = from player in list
                                select player.PlayerName;
                WarDeclarablePacket packet = new WarDeclarablePacket(warName, namesEnum.ToArray());
                context.PlayerSender!.SendPacket(packet);

                server.PlayerList.WarManager.PrepareNewWar(warName);
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.ClientWarDeclarableList.ChangeWarDeclarable(names);
            }
        }
    }
}
