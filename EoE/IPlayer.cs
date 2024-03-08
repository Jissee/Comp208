using EoE.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE
{
    public interface IPlayer
    {
        //Socket Connection { get; }
        string PlayerName { get; set; }
        bool IsConnected { get; }
        void SendPacketRaw(byte[] data);
        void Disconnect();
        int AvailableData();
        byte[] GetPacketBuf();
    }
}
