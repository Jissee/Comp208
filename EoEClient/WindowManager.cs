
using EoE.Client.GovernanceSystem;
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
using System.Windows.Documents;
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
            app.Run(INSTANCE.GetWindows<LoginWindow>());
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

        public T GetWindows<T>() where T : Window, new()
        {
            Type t = typeof(T);
            string typeName = t.FullName;

            if (!WindowsDict.ContainsKey(typeName))
            {
                WindowsDict.Add(typeName, new T());
            }
            return (T)WindowsDict[typeName];
        }

        public void ShowGameSettingWindow()
        {
            Application.Current.Dispatcher.Invoke(()=>
            {
                ShowWindows<SetGameWindow>();
                LoginWindow window = GetWindows<LoginWindow>();
                window.ignoreClosing = true;
                window.Close();
            });
            
        }

        public void ShowGameEntterWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowWindows<EnterGamePage>();
                LoginWindow window = GetWindows<LoginWindow>();
                window.ignoreClosing = true;
                window.Close();
            });
        }
        public void UpdateResources()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ResourceListRecord record = new ResourceListRecord(Client.INSTANCE.GonveranceManager.ResourceList);
                MainGamePage window = GetWindows<MainGamePage>();
                window.SynchronizeResources(record);
                MilitaryManagement military = GetWindows<MilitaryManagement>();
                military.SynchronizeResources(record);
            });
        }

        public void UpdateFields()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FieldListRecord record = new FieldListRecord(Client.INSTANCE.GonveranceManager.FieldList);
                BlockManagement blockManagement = GetWindows<BlockManagement>();
                blockManagement.SynchronizeFields(record);

            });

        }
        public void UpdatePopulation()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PopulationRecord record = Client.INSTANCE.GonveranceManager.PopManager.GetPopulationRecord();
                MainGamePage window = GetWindows<MainGamePage>();
                window.SynchronizePopulation(record);
                BlockManagement blockManagement = GetWindows<BlockManagement>();
                blockManagement.SynchronizePopulation(record);
            });
        }
        public void SynchronizeTickCount(int round)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGamePage window = GetWindows<MainGamePage>();
                window.SynchronizeRoundNumber(round);
                window.NextRound.IsChecked = false;
            });
        }
      

        public void ShowGameMainPage()
        {
            Type t = typeof(MainGamePage);
            string typeName = t.FullName;
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowWindows<MainGamePage>();
                EnterGamePage window = GetWindows<EnterGamePage>();
                window.ignoreClosing = true;
                window.Close();
            });
        }
        public void UpdateGameSetting(int playerNumber,int gameRound)
        {
            Type t = typeof(EnterGamePage);
            string typeName = t.FullName;
            Application.Current.Dispatcher.Invoke(() =>
            {
                EnterGamePage window = (EnterGamePage)GetWindows<EnterGamePage>();
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
