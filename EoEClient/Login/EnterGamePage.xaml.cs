using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EoE.Client.Login
{
    
    public partial class EnterGamePage : Window
    {
        private List<string> playerNames;
        private string currentPlayer;
        public EnterGamePage(List<string> players, string username)
        {
            InitializeComponent();
            playerNames = players;
            currentPlayer = username;
            InitializePlayerList();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //playerNames.Remove(currentPlayer);
            //PlayerListBox.Items.Clear(); //初始化
            //InitializePlayerList();

            //如果点击logout，关闭应用
            //if (currentPlayer == GetCurrentPlayerName())
            //{
                Application.Current.Shutdown();
           // }
        }

        private string GetCurrentPlayerName()
        {  
            return currentPlayer;
       }

        //实时显示玩家姓名，listbox
        private void InitializePlayerList()
        {
            PlayerListBox.Items.Add(CreatePlayerListItem(currentPlayer));

            foreach (string playerName in playerNames)
            {
                if (playerName != currentPlayer)
                {
                    PlayerListBox.Items.Add(CreatePlayerListItem(playerName));
                }
            }
        }


        private StackPanel CreatePlayerListItem(string playerName)
        {
            StackPanel playerItem = new StackPanel();
            playerItem.Orientation = Orientation.Horizontal;

            TextBlock playerNameTextBlock = new TextBlock();
            playerNameTextBlock.Text = playerName;
 
            playerItem.Children.Add(playerNameTextBlock);
            return playerItem;
        }

        private void EnterGame_Click(object sender, RoutedEventArgs e)
        {
           SetGameWindow setGameWindow = new SetGameWindow();
            setGameWindow.Show();
            this.Hide();
        }
    }
}
