using EoE.Client.GovernanceSystem;
using EoE.Client.Network;
using EoE.GovernanceSystem.Interface;
using EoE.Network;
using EoE.Network.Packets;
using EoE.Server.Network;
using EoE.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Socket Connection { get; private set; }
        public string PlayerName { get; }
        private bool isRunning;
        public PacketHandler Handler { get; }

        public IGonveranceManager GonveranceHandler { get; init; }
        public Client(string playerName) 
        {
            Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Handler = new ClientPacketHandler(this);
            PlayerName = playerName;
            GonveranceHandler = new ClientGoverance();
        }
        public void Connect(string host, int port)
        {
            lock (this)
            {
                try
                {
                    Connection.Connect(host, port);
                }catch (SocketException ex)
                {
                    MsgBox(ex.ToString());
                    Console.WriteLine(ex);
                    return;
                }
                
                isRunning = true;
                Task.Run(MessageLoop);
            }
            SendPacket(new PlayerLoginPacket(PlayerName));
        }
        public void Disconnect()
        {
            lock (this)
            {
                Connection.Close();
                isRunning = false;
                Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public void Stop()
        {
            lock (this)
            {
                Connection?.Close();
                isRunning = false;
            }
        }

        public void MessageLoop()
        {
            while (isRunning)
            {
                lock(this)
                {
                    if (Connection.Connected)
                    {
                        if (Connection.Available > 0)
                        {
                            byte[] lengthBuf = new byte[8];

                            Connection.Receive(lengthBuf);
                            MemoryStream msLen = new MemoryStream(lengthBuf);
                            BinaryReader br = new BinaryReader(msLen);
                            long length = br.ReadInt64();


                            byte[] buf = new byte[length];
                            int i = Connection.Receive(buf);
                            Console.WriteLine(i);
                            PacketContext context = new PacketContext(NetworkDirection.Server2Client, null, this);
                            Handler.ReceivePacket(buf, context);

                        }
                    }
                }
            }
        }

        public void SendPacket<T>(T packet) where T : IPacket<T>
        {
            lock(this)
            {
                Handler.SendPacket(packet, this.Connection, null);
            }
        }

        public void MsgBox(string msg)
        {
            Task.Run(() =>
            {
                MessageBox.Show(msg);
            });
            
        }

    }
}
