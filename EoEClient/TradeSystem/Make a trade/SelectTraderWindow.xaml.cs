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
using WpfApp1.Login.TradeSystem.Make_a_trade;

namespace EoE.Client.TradeSystem
{
    /// <summary>
    /// SelectTraderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTraderWindow : Window
    {
        private List<string> playerNames;
        private string currentPlayer;
        //private List<string> playerNames = new List<string> { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6" };
        public SelectTraderWindow()
        {
            InitializeComponent();
            playerNames = new List<string>();
            InitializePlayerList();
        }
        public SelectTraderWindow(List<string> players, string username)
        {
            InitializeComponent();


            playerNames = players ?? new List<string>();

            currentPlayer = username;
            InitializePlayerList();
        }


        private string GetCurrentPlayerName()
        {
            //return "Player1"; 
            return currentPlayer;
        }

        private void InitializePlayerList()
        {
            //string currentPlayer = GetCurrentPlayerName();

            foreach (string playerName in playerNames)
            {
                if (playerName != currentPlayer)
                {
                    PlayerListBox.Items.Add(CreatePlayerListItem(playerName));
                }
            }
        }

        private void PlayerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedPlayer = PlayerListBox.SelectedItem as string;

            // 如果有玩家被选中
            if (selectedPlayer != null)
            {
                // 调用选择玩家按钮点击事件处理程序，传递当前选中的玩家作为参数
                SelectPlayerButton_Click(selectedPlayer);
            }
        }

        private StackPanel CreatePlayerListItem(string playerName)
        {
            StackPanel playerItem = new StackPanel();
            playerItem.Orientation = Orientation.Horizontal;

            TextBlock playerNameTextBlock = new TextBlock();
            playerNameTextBlock.Text = playerName;

            Button selectButton = new Button();
            selectButton.Content = "Choose";
            selectButton.Click += (sender, e) => SelectPlayerButton_Click(playerName);

            playerItem.Children.Add(playerNameTextBlock);
            playerItem.Children.Add(selectButton);
           

            return playerItem;
        }

        private void SelectPlayerButton_Click(string playerName)
        {
            // 在这里实现跳转到选择物品界面的逻辑
            //List<TradeItem> selectedItems = GetSelectedItems();
            SellAndBuyWindow sellAndBuyWindow = new SellAndBuyWindow();
            sellAndBuyWindow.Show();
            
            this.Hide();
        }

        //private List<TradeItem> GetSelectedItems()
        //{
            //throw new NotImplementedException();
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MainTradePage mainTradePage = new MainTradePage();
           mainTradePage.Show();
            this.Close();

        }

       
        
    }
}
