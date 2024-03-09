using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public interface IClient : INetworkEntity
    {
        string PlayerName { get; }
        void MsgBox(string msg);
        void AddRemotePlayer(string playerName);
        void RemoveRemotePlayer(string playerName);
    }
}
