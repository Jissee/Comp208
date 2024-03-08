using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client
{
    internal class RemotePlayer : IPlayer
    {
        public RemotePlayer(string playerName) 
        {
            PlayerName = playerName;
        }
        public string PlayerName { get; set; }

        public bool IsConnected => true;

        public int AvailableData()
        {
            throw new NotSupportedException("Cannot check a RemotePlayer");
        }

        public void Disconnect()
        {
            throw new NotSupportedException("Cannot check a RemotePlayer");
        }

        public byte[] GetPacketBuf()
        {
            throw new NotSupportedException("Cannot check a RemotePlayer");
        }

        public void SendPacketRaw(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
