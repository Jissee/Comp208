using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Packets
{
    public class RemotePlayerSyncPacket : IPacket<RemotePlayerSyncPacket>
    {
        private string playerName;
        private bool status;
        public RemotePlayerSyncPacket(string playerName, bool status)
        {
            this.playerName = playerName;
            this.status = status;
        }
        public static RemotePlayerSyncPacket Decode(BinaryReader reader)
        {
            return new RemotePlayerSyncPacket(reader.ReadString(), reader.ReadBoolean());
        }

        public static void Encode(RemotePlayerSyncPacket obj, BinaryWriter writer)
        {
            writer.Write(obj.playerName);
            writer.Write(obj.status);
        }

        public void Handle(PacketContext context)
        {
            if(context.NetworkDirection == NetworkDirection.Server2Client)
            {
                IClient client = (IClient)context.Receiver;
                if(status ==  false)
                {
                    client.RemoveRemotePlayer(playerName);
                    client.MsgBox($"{playerName} left the game.");
                }
                else
                {
                    client.AddRemotePlayer(playerName);
                    client.MsgBox($"{playerName} joined the game.");
                }
                
            }
        }
    }
}
