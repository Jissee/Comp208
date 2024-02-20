using EoE.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Server
{
    public class DedicatedServer
    {
        private Socket serverSocket;
        private IPEndPoint address;
        private List<Socket> clients;
        private bool isRunning;
        private PacketHandler handler;
        public DedicatedServer(string ip, int port) 
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = new IPEndPoint(IPAddress.Parse(ip), port);
            clients = new List<Socket>();
            //handler = new PacketHandler(this);
        }

        public void Start()
        {
            lock(this)
            {
                serverSocket.Bind(address);
                serverSocket.Listen(6);
                isRunning = true;
                Task.Run(ConnectionLoop);
                Task.Run(DisConnectionLoop);
                Task.Run(MessageLoop);
            }
        }
        public void Stop()
        {
            lock(this)
            {
                serverSocket?.Close();
                isRunning = false;
            }
        }

        public void ConnectionLoop()
        {
            while (isRunning)
            {
                Socket cl = serverSocket.Accept();
                lock (clients)
                {
                    clients.Add(cl);
                }
            }
        }
        public void DisConnectionLoop()
        {
            while (isRunning)
            {
                lock (clients)
                {
                    for(int i = 0; i < clients.Count; i++)
                    {
                        Socket c = clients[i];
                        if (!c.Connected)
                        {
                            Console.WriteLine(c + "disconnected");
                        }
                        clients.Remove(c);
                    }
                }
            }
        }

        public void MessageLoop()
        {
            while (isRunning)
            {
                lock (clients)
                {
                    foreach (Socket cl in clients)
                    {
                        if (cl.Available > 0)
                        {
                            byte[] buf = new byte[cl.Available];
                            int i = cl.Receive(buf);
                            Console.WriteLine(i);

                            //ph.ReceivePacket(buf);
                        }
                    }
                }
            }
        }

        public void SendData(byte[] data, int playerId)
        {
            lock (clients)
            {
                if(playerId > clients.Count)
                {
                    return;
                }
                else
                {
                    Socket cl = clients[playerId];
                    cl.Send(data);
                }
            }

        }

    }
}
