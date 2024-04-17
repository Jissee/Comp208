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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<AllocateArmy>();
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
