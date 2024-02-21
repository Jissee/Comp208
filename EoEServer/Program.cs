using EoE.Network;
using EoE.Server.Network;

namespace EoE.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("0.0.0.0", 25566);
            server.Start();

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
