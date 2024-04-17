
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
                ShowWindows<EnterGameWindow>();
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
                MainGameWindow window = GetWindows<MainGameWindow>();
                window.SynchronizeResources(record);
                MilitaryManagementWindow military = GetWindows<MilitaryManagementWindow>();
                military.SynchronizeResources(record);
                ResourceInformationWindow resourceManage = GetWindows<ResourceInformationWindow>();
                resourceManage.SynchronizeResources(record);
            });
        }

        public void UpdateFields()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FieldListRecord record = new FieldListRecord(Client.INSTANCE.GonveranceManager.FieldList);
                BlockManagementWindow blockManagement = GetWindows<BlockManagementWindow>();
                blockManagement.SynchronizeFields(record);
                ResourceInformationWindow resourceManage = GetWindows<ResourceInformationWindow>();
                resourceManage.SynchronizeFields(record);

                CheckOtherPlayer checkOther = GetWindows<CheckOtherPlayer>();
                checkOther.SynchronizeOtherPlayersFields();
            });

        }
        public void UpdatePopulation()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PopulationRecord record = Client.INSTANCE.GonveranceManager.PopManager.GetPopulationRecord();
                MainGameWindow window = GetWindows<MainGameWindow>();
                window.SynchronizePopulation(record);
                BlockManagementWindow blockManagement = GetWindows<BlockManagementWindow>();
                blockManagement.SynchronizePopulation(record);
                ResourceInformationWindow resourceManage = GetWindows<ResourceInformationWindow>();
                resourceManage.SynchronizePopulation(record);
                SetExploreWindow setExploreWindow = GetWindows<SetExploreWindow>();
                setExploreWindow.SynchronizePopulation(record);
            });
        }
        public void SynchronizeTickCount(int round)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MainGameWindow window = GetWindows<MainGameWindow>();
                window.SynchronizeRoundNumber(round);
                window.NextRound.IsChecked = false;
            });
        }
      

        public void ShowGameMainPage()
        {
            Type t = typeof(MainGameWindow);
            string typeName = t.FullName;
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowWindows<MainGameWindow>();
                EnterGameWindow window = GetWindows<EnterGameWindow>();
                window.ignoreClosing = true;
                window.Close();
            });
        }
        public void UpdateGameSetting(int playerNumber,int gameRound)
        {
            Type t = typeof(EnterGameWindow);
            string typeName = t.FullName;
            Application.Current.Dispatcher.Invoke(() =>
            {
                EnterGameWindow window = (EnterGameWindow)GetWindows<EnterGameWindow>();
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
