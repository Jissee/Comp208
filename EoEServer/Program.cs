using EoE.Network;
using EoE.Server;

namespace EoE.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DedicatedServer dserver = new DedicatedServer("0.0.0.0", 25566);
            ServerPacketHandler sph = new ServerPacketHandler(dserver);
            dserver.Start();
            while (true)
            {
                string str = Console.ReadLine();
                if(str == "stop") 
                {
                    break;
                }
            }
        }
    }
}
