using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class ServerPlayer : IPlayer
    {
        public Socket Connection { get; }
        private string name;
        public string PlayerName { 
            get 
            {
                return name;
            }
            set 
            { 
                if (name == null)
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Player name cannot be reset.");
                }
            } 
        }
        public bool IsAvailable => PlayerName != null;

        public ServerPlayer(Socket connection)
        {
            this.Connection = connection;
        }
    }
}
