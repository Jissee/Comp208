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

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// CheckOtherFieldsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckOtherFieldsWindow : Window
    {
        public CheckOtherFieldsWindow()
        {
            InitializeComponent();
            addPlayer();
        }
        private void addPlayer()
        {
            foreach (string playerName in Client.INSTANCE.OtherPlayer)
            {
                playList.Items.Add(playerName);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
