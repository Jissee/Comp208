using EoE.Client.ChatSystem;
using EoE.Client.GovernanceSystem;
using EoE.Client.Network;
using EoE.Client.TradeSystem;
using EoE.Client.WarSystem;
using EoE.GovernanceSystem.ClientInterface;
using EoE.Network;
using EoE.Network.Entities;
using EoE.Network.Packets;
using EoE.Network.Packets.GameEventPacket;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.TradeSystem;
using EoE.WarSystem.Interface;
using System.IO;
using System.Net.Sockets;
using System.Windows;

namespace EoE.Client
{
    public class Client : IClient
    {
        public static Client INSTANCE { get; }
        public static void ShowException(string type, string message, Exception e)
        {
            MessageBox.Show($"[{type}] {message}:\n{e.ToString()}");
        }
        public Socket Connection { get; private set; }
        public string? PlayerName { get; private set; }
        private bool isRunning;
        public int TickCount { get; private set; }
        private PacketHandler Handler { get; }
        public List<string> OtherPlayer { get; private set; }
        public Dictionary<string, FieldListRecord> OtherPlayerFields { get; init; }
        public IClientGonveranceManager GonveranceManager { get; init; }
        public IClientTradeManager TradeManager { get; init; }
        public IClientWarManager WarManager { get; init; }
        public IClientTreatyList ClientTreatyList { get; set; }
        private Dictionary<string, List<string>> chat;
        public WindowManager WindowManager { get; init; }
        IWindowManager IClient.WindowManager => WindowManager;


        static Client()
        {
            INSTANCE = new Client();
        }

        private Client()
        {
            Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Handler = new ClientPacketHandler();
            OtherPlayer = new List<string>();
            GonveranceManager = new ClientGoverance();
            TradeManager = new ClientTradeManager();
            WarManager = new ClientWarManager();

            ClientTreatyList = new ClientTreatyList();
            WindowManager = EoE.Client.WindowManager.INSTANCE;
            OtherPlayerFields = new Dictionary<string, FieldListRecord>();
            chat = new Dictionary<string, List<string>>();
        }

        public void AddOthersChatMessage(string senderName, string message)
        {
            string regularizationMessage = DateTime.Now.ToString() + "  " + senderName + ": " + message;
            if (chat.ContainsKey(senderName))
            {
                chat[senderName].Add(regularizationMessage);
            }
            else
            {
                List<string> messageList = [regularizationMessage];
                chat.Add(senderName, messageList);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                ChatWindow chat = WindowManager.INSTANCE.GetWindows<ChatWindow>();
                chat.SynchronizeChat(senderName);
            });
        }

        public void AddSelfChatMessage(string receiver, string message)
        {
            string regularizationMessage = DateTime.Now.ToString() + "  " + "You: " + message;
            if (chat.ContainsKey(receiver))
            {
                chat[receiver].Add(regularizationMessage);
            }
            else
            {
                List<string> messageList = [regularizationMessage];
                chat.Add(receiver, messageList);
            }
        }

        public List<string>? GetChatMessage(string name)
        {
            if (chat.ContainsKey(name))
            {
                return chat[name];
            }
            else
            {
                return null;
            }
        }
        public void SynchronizeTickCount(int tickCount)
        {
            WindowManager.SynchronizeTickCount(tickCount);
        }
        public void SetPlayerName(string name)
        {
            PlayerName = name;
        }
        public void SynchronizeOtherPlayersName(List<string> otherPlayers)
        {
            OtherPlayer.Clear();
            foreach (string name in otherPlayers)
            {
                if (name != PlayerName)
                {
                    OtherPlayer.Add(name);
                }
            }
            WindowManager.INSTANCE.SynchronizeOtherPlayersName();


        }
        public void SynchronizePlayerName(string name)
        {
            PlayerName = name;
            WindowManager.INSTANCE.SynchronizeOtherPlayersName();
        }

        public void SynchronizeOtherPlayerFieldLitst(string name, FieldListRecord record)
        {
            if (name != PlayerName)
            {
                if (OtherPlayerFields.ContainsKey(name))
                {
                    OtherPlayerFields[name] = record;
                }
                else
                {
                    OtherPlayerFields.Add(name, record);
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CheckOtherPlayer window = WindowManager.INSTANCE.GetWindows<CheckOtherPlayer>();
                    window.SynchronizeOtherPlayersFields();
                });
            }

        }

        public void Connect(string host, int port)
        {
            lock (this)
            {
                try
                {
                    Connection.Connect(host, port);
                }
                catch (Exception ex)
                {
                    ShowException("Connection", "Client connection failed. Please check the IP address and port number", ex);
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
                lock (this)
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
            lock (this)
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
            if (MessageBox.Show(msg, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
