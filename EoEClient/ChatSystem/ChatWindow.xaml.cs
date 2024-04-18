using EoE.Client.TradeSystem;
using EoE.Network.Packets.Chat;
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
using System.Xml.Linq;

namespace EoE.Client.ChatSystem
{
    /// <summary>
    /// chat.xaml 的交互逻辑
    /// </summary>
    public partial class ChatWindow : Window
    {

        public ChatWindow()
        {
            InitializeComponent();
            SynchronizeOtherPlayerList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<EstablishRelationWindow>();
        }

        public void SynchronizeOtherPlayerList()
        {
            selectedName.Items.Clear();
            foreach (string playerName in Client.INSTANCE.OtherPlayer)
            {
                selectedName.Items.Add(playerName);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SelectTraderWindow>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void selectedName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ShowMessage();
        }

        private void ShowMessage()
        {
            if (selectedName.SelectedItem != null)
            {
                messageBoard.Items.Clear();
                string name = selectedName.SelectedItem.ToString();
                if (Client.INSTANCE.GetChatMessage(name) != null)
                {
                    List<string> messages = Client.INSTANCE.GetChatMessage(name);

                    foreach (string message in messages)
                    {
                        messageBoard.Items.Add(message);
                    }

                }
            }
        }

        public void SynchronizeChat(string messageSender)
        {
            if (selectedName.SelectedItem != null)
            {
                if (messageSender == selectedName.SelectedItem.ToString())
                {
                    ShowMessage();
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (selectedName.SelectedItem != null)
            {
                if (chetBox.Text != null)
                {
                    Client.INSTANCE.AddSelfChatMessage(selectedName.SelectedItem.ToString(), chetBox.Text);
                    ShowMessage();
                    Client.INSTANCE.SendPacket(new ChatPacket(chetBox.Text, selectedName.SelectedItem.ToString(), Client.INSTANCE.PlayerName));
                }
                else
                {
                    MessageBox.Show("Can't send empty message");
                }
            }
            else
            {
                MessageBox.Show("Please select a player to chat");
            }
                
        }
    }
}
