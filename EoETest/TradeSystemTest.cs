using EoE.Server.GovernanceSystem;
using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EoE.GovernanceSystem;
using EoE.Server.WarSystem;
using EoE.Server;
using EoE.GovernanceSystem.ServerInterface;
using EoE.TradeSystem;
using System.Transactions;

namespace EoE.Test
{
    public class TradeSystemTest
    {
        [TestMethod]
        public void TestPopAllocation()
        {
            Server.Server server = new Server.Server("0.0.0.0", 25566);
            Client.Client client = new Client.Client();
            IPlayer player = new ServerPlayer(server.ServerSocket, server);
            server.PlayerList.Players.Add(player);
            server.BeginGame();
            IServerTradeManager serverTrade = server.PlayerList.TradeManager;

            //GameTransaction transaction = new GameTransaction("a",0.);
           //serverTrade.AcceptOpenTransaction();

        }
       
    }
}
