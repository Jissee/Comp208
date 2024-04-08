using EoE.GovernanceSystem.Interface;
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
        public IGonveranceManager GonveranceHandler { get; }
        void MsgBox(string msg);
    }
}
