using EoE.Network;
using EoE.Server.Network;

namespace EoE.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server; 

            while (true)
            {
                server= new Server("0.0.0.0", 25566);
                server.Start();

                while (!server.IsNeedRestart())
                { 
                }

            }
        }
    }
}
