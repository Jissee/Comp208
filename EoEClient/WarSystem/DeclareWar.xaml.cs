using EoE.Network.Packets.WarPacket;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.WarSystem
{
    /// <summary>
    /// DeclareWar.xaml 的交互逻辑
    /// </summary>
    public partial class DeclareWar : Window
    {
        public DeclareWar()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WarMainPage window = WindowManager.INSTANCE.GetWindows<WarMainPage>();
            var selectedName = listBox1.SelectedItem;
            if (selectedName != null)
            {
                WarIntensionPacket packet = new WarIntensionPacket(WarMainPage.theWarName, selectedName.ToString()!, [], []);
                Client.INSTANCE.SendPacket(packet);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                WarDeclarationPacket packet = new WarDeclarationPacket(WarMainPage.theWarName);
                Client.INSTANCE.SendPacket(packet);
                this.Hide();
                MessageBox.Show($"""
                                You begin the war with {listBox1.SelectedItem.ToString()}!
                                Remember to fill in the frontier!
                                OtherWise, you will automatically surrender!!!
                                """);
            }
            else
            {
                MessageBox.Show("Please select an enemy from list.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                WarInvitationPacket packet = new WarInvitationPacket(WarMainPage.theWarName, listBox2.SelectedItem.ToString()!);
                Client.INSTANCE.SendPacket(packet);
            }
        }

        public void ChangeWarDeclarableList(string[] names)
        {
            listBox1.Items.Clear();
            foreach (string theName in names)
            {
                listBox1.Items.Add(theName);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox2.Items.Clear();
        }
    }
}
