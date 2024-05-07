using EoE.Network.Entities;

namespace EoE.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                IServer.Log("Server", "Insufficient argument.");
            }
            Server server;

            while (true)
            {

                server = new Server(args[0], int.Parse(args[1]));
                server.Start();

                while (!server.IsNeedRestart())
                {
                    //int pausehere = 0;
                }
                server.Stop();
                IServer.Log("Server", "Server restarting");
            }
        }
    }
}
