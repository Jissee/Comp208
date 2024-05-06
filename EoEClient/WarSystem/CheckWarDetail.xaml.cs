using EoE.Network.Packets.WarPacket;
using System.Windows;

namespace EoE.Client.WarSystem
{
    /// <summary>
    /// CheckWarDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CheckWarDetail : Window
    {
        public CheckWarDetail()
        {
            InitializeComponent();
            BattleWe.IsReadOnly = true;
            InfoWe.IsReadOnly = true;
            MechWe.IsReadOnly = true;
            BattleLostWe.IsReadOnly = true;
            InfoLostWe.IsReadOnly = true;
            MechLostWe.IsReadOnly = true;

            BattleEnemy.IsReadOnly = true;
            InfoEnemy.IsReadOnly = true;
            MechEnemy.IsReadOnly = true;
            BattleLostEnemy.IsReadOnly = true;
            InfoLostEnemy.IsReadOnly = true;
            MechLostEnemy.IsReadOnly = true;
            WarName.Text = CheckStatus.selectedWarName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<AllocateArmy>();
            CheckStatus window = WindowManager.INSTANCE.GetWindows<CheckStatus>();
            WarWidthQueryPacket packet = new WarWidthQueryPacket(CheckStatus.selectedWarName, 0);
            Client.INSTANCE.SendPacket(packet);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
