
using EoE.Client.ChatSystem;
using EoE.Client.GovernanceSystem;
using EoE.Client.Login;
using EoE.Client.TradeSystem;
using EoE.Client.WarSystem;
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
            Window window = GetWindows<T>();
            window.Show();
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
        public void CloseWindow<T>() where T : Window, new()
        {
            Type t = typeof(T);
            string typeName = t.FullName;
            Window window = GetWindows<T>();
            window.Close();
            WindowsDict.Remove(typeName);
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

        public void SynchronizeOtherPlayersName()
        {
            Application.Current.Dispatcher.Invoke((() =>
            {
                EnterGameWindow entetPage = GetWindows<EnterGameWindow>();
                entetPage.SynchronizeOtherPlayerList();
                SelectTraderWindow selectTrader = GetWindows<SelectTraderWindow>();
                selectTrader.SynchronizeOtherPlayerList();
                MainGameWindow mainGameWindow = GetWindows<MainGameWindow>();
                mainGameWindow.SynchronizeOtherPlayerList();
                ChatWindow chatWindow = GetWindows<ChatWindow>();
                chatWindow.SynchronizeOtherPlayerList();
                WarDetail warDetail = GetWindows<WarDetail>();
                warDetail.SynchronizeOtherPlayerList();
            }));
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
                AllocateArmy allocateArmy = GetWindows<AllocateArmy>();
                allocateArmy.SynchronizeResources(record);
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

        
    }
}
