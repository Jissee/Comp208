using EoE.Client.GovernanceSystem;
using EoE.Client.Login;
using EoE.Client.Network;
using EoE.Client.WarSystem;
using EoE.ClientInterface;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.WarSystem.Interface;
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
    public class Client : IClient
    {
        public static Client INSTANCE { get; }
        public Socket Connection { get; private set; }
        public string? PlayerName { get; private set; }
        private bool isRunning;

        public PacketHandler Handler { get; }
        public List<string> OtherPlayer { get; private set; } 
        public IClientGonveranceManager GonveranceManager { get; init; }

        public IClientWarDeclarableList ClientWarDeclarableList {  get; set; }

        public IClientWarInformationList ClientWarInformationList {  get; set; }

        public IClientWarProtectorsList ClientWarProtectorsList {  get; set; }

        public IClientWarParticipatibleList ClientWarParticipatibleList {  get; set; }
        public IClientWarTargetList ClientWarTargetList { get; set; }
        public IClientTreatyList ClientTreatyList {  get; set; }
        IWindowManager IClient.WindowManager => WindowManager;

        public WindowManager WindowManager { get; init; }

        static Client() 
        {
            INSTANCE = new Client();
        }

        //TODO for test,change to public temporarily
        public Client() 
        {
            Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Handler = new ClientPacketHandler();
            OtherPlayer = new List<string>();
            GonveranceManager = new ClientGoverance();
            ClientWarDeclarableList = new ClientWarDeclarableList();
            ClientWarInformationList = new ClientWarInformationList();
            ClientWarProtectorsList = new ClientWarProtectorsList();
            ClientWarParticipatibleList = new ClientWarParticipatibleList();
            ClientWarTargetList = new ClientWarTargetList();
            ClientTreatyList = new ClientTreatyList();
            WindowManager = EoE.Client.WindowManager.INSTANCE;
        }
        public void SetPlayerName(string name)
        {
            PlayerName = name;
        }
        public void SynchronizePlayerName(List<string> otherPlayers)
        {
            OtherPlayer.Clear();
            foreach (string name in otherPlayers)
            {
                if (name != PlayerName)
                {
                    OtherPlayer.Add(name);
                }
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                EnterGamePage entetPage = (EnterGamePage)WindowManager.GetWindows<EnterGamePage>();
                entetPage.SynchronizePlayerList();
            });
            
        }
        public void SynchronizePlayerName(string name)
        {
            PlayerName = name;
            Application.Current.Dispatcher.Invoke(() =>
            {
                EnterGamePage entetPage = (EnterGamePage)WindowManager.GetWindows<EnterGamePage>();
                entetPage.SynchronizePlayerList();
            });
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
                Connection.Shutdown(SocketShutdown.Both);
                Thread.Sleep(500);
                Connection.Close();
                isRunning = false;
                Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            App.Current.Shutdown();
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
                            Handler.ReceivePacket(buf, context, "server");

                        }
                    }
                }
            }
        }

        public void SendPacket<T>(T packet) where T : IPacket<T>
        {
            lock(this)
            {
                Handler.SendPacket(packet, this.Connection, "server");
            }
        }

        public void MsgBox(string msg)
        {
            Task.Run(() =>
            {
                MessageBox.Show(msg);
            });
        }

        public bool MsgBoxYesNo(string msg)
        {
            if(MessageBox.Show(msg,"",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
