using EoE.Network.Entities;

namespace EoE.Network.Packets.WarPacket
{
    public class WarNameQueryPacket : IPacket<WarNameQueryPacket>
    {
        string[] warNames;
        public WarNameQueryPacket(string[] warNames)
        {
            this.warNames = warNames;
        }
        public static WarNameQueryPacket Decode(BinaryReader reader)
        {
            int cut = reader.ReadInt32();
            string[] names = new string[cut];
            for (int i = 0; i < cut; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarNameQueryPacket(names);
        }

        public static void Encode(WarNameQueryPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.warNames.Length);
            for (int i = 0; i < obj.warNames.Length; i++)
            {
                writer.Write(obj.warNames[i]);
            }
        }

        public void Handle(PacketContext context)
        {
            if (context.NetworkDirection == NetworkDirection.Client2Server)
            {
                IServer server = (IServer)context.Receiver;
                IPlayer player = context.PlayerSender!;
                List<string> names = new List<string>();
                foreach (string theWarName in server.PlayerList.WarManager.WarDict.Keys)
                {
                    names.Add(theWarName);
                }
                WarNameQueryPacket packet = new WarNameQueryPacket([.. names]);
                player.SendPacket(packet);
            }
            else
            {
                IClient client = (IClient)context.Receiver;
                client.WarManager.ClientWarNameList.ChangeWarNames(warNames);
            }
        }
    }
}
