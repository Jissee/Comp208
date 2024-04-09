using EoE.GovernanceSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network.Entities
{
    public interface IClient : INetworkEntity
    {
        string PlayerName { get; }
        public IClietGonveranceManager GonveranceHandler { get; }
        void MsgBox(string msg);
    }
}
