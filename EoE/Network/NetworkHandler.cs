using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Network
{
    public class NetworkHandler1
    {
        private Socket sv;
        private Socket cl;
        private List<Socket> clients = new List<Socket>();
        private bool isServerRunning = true;
        public PacketHandler ph;

        public NetworkHandler1() 
        { 
            //ph = new PacketHandler();
        }
        

        public void Connect(string host, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(host), port);
            cl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cl.Connect(endPoint);

        }

        public void StartServer(string host, int port)
        {
            IPEndPoint svadd = new IPEndPoint(IPAddress.Parse(host), port);
            sv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sv.Bind(svadd);
            sv.Listen(6);
            Task.Run(ServerConnectionLoop);
            Task.Run(ServerMessageLoop);

        }

        public void CLSendPacket(byte[] data)
        {
            cl.Send(data);
        }

        private void ServerConnectionLoop()
        {
            while (isServerRunning)
            {
                Socket cl = sv.Accept();
                lock(clients)
                {
                    clients.Add(cl);
                }
                
            }

        }

        private void ServerMessageLoop() 
        {
            while (isServerRunning)
            {
                lock(clients)
                {
                    foreach(Socket cl in clients)
                    {
                        if(cl.Available > 0)
                        {
                            byte[] buf = new byte[cl.Available];
                            int i = cl.Receive(buf);
                            Console.WriteLine(i);

                            ph.ReceivePacket(buf);
                        }
                    }
                }
            }
        }


    }
}
