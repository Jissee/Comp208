using EoE.Client.Network;
using EoE.Network;
using EoE.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EoE.Client
{
    internal class Client : IClient
    {
        public Socket Socket { get; private set; }
        public string PlayerName { get; }
        private bool isRunning;
        private ClientPacketHandler handler;
        public Client(string playerName) 
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            handler = new ClientPacketHandler(this);
            PlayerName = playerName;
        }
        public void Connect(string host, int port)
        {
            lock (this)
            {
                try
                {
                    Socket.Connect(host, port);
                }catch (SocketException ex)
                {
                    MsgBox(ex.ToString());
                    Console.WriteLine(ex);
                    return;
                }
                
                isRunning = true;
                Task.Run(MessageLoop);
            }
            SendPacket(new ClientLoginPacket(PlayerName));
        }
        public void Disconnect()
        {
            lock (this)
            {
                Socket.Close();
                isRunning = false;
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public void Stop()
        {
            lock (this)
            {
                Socket?.Close();
                isRunning = false;
            }
        }

        public void MessageLoop()
        {
            while (isRunning)
            {
                lock(this)
                {
                    if (Socket.Connected)
                    {
                        if (Socket.Available > 0)
                        {
                            byte[] buf = new byte[Socket.Available];
                            int i = Socket.Receive(buf);
                            Console.WriteLine(i);
                            PacketContext context = new PacketContext(NetworkDirection.Server2Client, null, this);
                            handler.ReceivePacket(buf, context);

                        }
                    }
                }
            }
        }

        public void SendPacket<T>(T packet) where T : IPacket<T>
        {
            lock(this)
            {
                handler.SendPacket(packet);
            }
        }

        public void MsgBox(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
