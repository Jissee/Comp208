
using EoE.Client.Login;
using EoE.ClientInterface;
using EoE.Network.Packets.GonverancePacket.Record;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace EoE.Client
{
    public class WindowManager: IWindowManager
    {
        public static WindowManager INSTANCE { get; private set; }

        private Dictionary<string,Window> WindowsDict = new Dictionary<string,Window>();
        static WindowManager()
        {
            INSTANCE = new WindowManager();
        }

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LoginWindow());
        }
        public void ShowWindows<T>() where T : Window, new()
        {
            Type t = typeof(T);
            string typeName = t.FullName;

            if (!WindowsDict.ContainsKey(typeName)) 
            {
                WindowsDict.Add(typeName, new T());
            }
            Window window = WindowsDict[typeName];
            if (!window.IsVisible)
            {
                window.Show();
            }
            
        }

        public Window GetWindows<T>() where T : Window, new()
        {
            Type t = typeof(T);
            string typeName = t.FullName;

            if (!WindowsDict.ContainsKey(typeName))
            {
                WindowsDict.Add(typeName, new T());
            }
            return WindowsDict[typeName];
        }

        public void ShowGameSettingWindow()
        {
            Application.Current.Dispatcher.Invoke(()=>
            {
                ShowWindows<SetGameWindow>();
            });
            
        }

        public void UpdateOtherPlayerField(string playerName, FieldListRecord record)
        {

        }
        public void UpdateGameSetting(int playerNumber,int gameRound)
        {
            Type t = typeof(EnterGamePage);
            string typeName = t.FullName;
            EnterGamePage window = (EnterGamePage)WindowsDict[typeName];
            Application.Current.Dispatcher.Invoke(() =>
            {
                window.SynchronizeGameSetting(playerNumber, gameRound);
            });

        }

        public static void shutDown(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("If you close this window, the program will stop running. Are you sure you want to close it?", "Close Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                App.Current.Shutdown();
            }
        }
    }
}
