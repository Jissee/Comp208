using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("12313213");
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            
            System.Console.ReadKey();
        }
    }
}
