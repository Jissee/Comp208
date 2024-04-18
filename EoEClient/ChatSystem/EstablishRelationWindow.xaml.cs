using EoE.Client.GovernanceSystem;
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
using EoE.Client.ChatSystem;
using EoE.Client.WarSystem;
using System.Text.RegularExpressions;
using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.TreatyPacket;

namespace EoE.Client.ChatSystem
{
    /// <summary>
    /// EstablishRelation.xaml 的交互逻辑
    /// </summary>
    public partial class EstablishRelationWindow : Window
    {
        public EstablishRelationWindow()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(protecting.IsChecked == true)
            {
                _protected.IsChecked = false;
                common.IsChecked = false;
            }
            Res1.IsReadOnly = false;
            Res2.IsReadOnly = false;
            Res3.IsReadOnly = false;
            Res4.IsReadOnly = false;
            Res5.IsReadOnly = false;
            Res6.IsReadOnly = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!((bool)_protected.IsChecked || (bool)common.IsChecked|| (bool)protecting.IsChecked))
            {
                MessageBox.Show("Please select a treaty!");
            }
            ChatPage window = WindowManager.INSTANCE.GetWindows<ChatPage>();
            if (window.selectedName.SelectedItem != null)
            {
                ResourceListRecord record = new ResourceListRecord();
                record.siliconCount = int.Parse(Res1.Text);
                record.copperCount = int.Parse(Res2.Text);
                record.ironCount = int.Parse(Res3.Text);
                record.aluminumCount = int.Parse(Res4.Text);
                record.electronicCount = int.Parse(Res5.Text);
                record.industrialCount = int.Parse(Res6.Text);
                if ((bool)protecting.IsChecked)
                {
                    NewProtectiveTreatyPacket packet = new NewProtectiveTreatyPacket(
                        record,
                        false,
                        Client.INSTANCE.PlayerName!,
                        window.selectedName.SelectedItem.ToString()!
                        );
                    Client.INSTANCE.SendPacket( packet );
                }
                if ((bool)_protected.IsChecked)
                {
                    NewProtectiveTreatyPacket packet = new NewProtectiveTreatyPacket(
                        record,
                        true,
                        Client.INSTANCE.PlayerName!,
                        window.selectedName.SelectedItem.ToString()!
                        );
                    Client.INSTANCE.SendPacket(packet);
                }
                if ((bool)common.IsChecked)
                {
                    NewCommonDefenseTreatyPacket packet = new NewCommonDefenseTreatyPacket(
                        Client.INSTANCE.PlayerName!,
                        window.selectedName.SelectedItem.ToString()!
                        );
                }
            }
            else
            {
                MessageBox.Show("You have not chosen who to sign the treaty!");
            }
        }

        private void _protected_Checked(object sender, RoutedEventArgs e)
        {
            if (_protected.IsChecked == true)
            {
                protecting.IsChecked = false;
                common.IsChecked = false;
            }
            Res1.IsReadOnly = false;
            Res2.IsReadOnly = false;
            Res3.IsReadOnly = false;
            Res4.IsReadOnly = false;
            Res5.IsReadOnly = false;
            Res6.IsReadOnly = false;
        }

        private void common_Checked(object sender, RoutedEventArgs e)
        {
            if (common.IsChecked == true)
            {
                protecting.IsChecked = false;
                _protected.IsChecked = false;
            }
            Res1.IsReadOnly = true;
            Res2.IsReadOnly = true;
            Res3.IsReadOnly = true;
            Res4.IsReadOnly = true;
            Res5.IsReadOnly = true;
            Res6.IsReadOnly = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<TreatyContentWindow>();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<ResourceInformationWindow>();
        }
    }
}
