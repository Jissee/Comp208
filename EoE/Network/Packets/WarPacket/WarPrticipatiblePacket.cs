using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets.WarPacket
{
    public class WarPrticipatiblePacket : IPacket<WarPrticipatiblePacket>
    {
        public string[] names;
        public WarPrticipatiblePacket(string[] names)
        {
            this.names = names;
        }
        public static WarPrticipatiblePacket Decode(BinaryReader reader)
        {
            int cnt = reader.ReadInt32();
            string[] names = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                names[i] = reader.ReadString();
            }
            return new WarPrticipatiblePacket(names);
        }

        public static void Encode(WarPrticipatiblePacket obj, BinaryWriter writer)
        {
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

            }
            else
            {

            }
        }
    }
}
