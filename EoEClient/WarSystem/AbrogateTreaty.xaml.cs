using EoE.Client.ChatSystem;
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
    /// AbrogateTreaty.xaml 的交互逻辑
    /// </summary>
    public partial class AbrogateTreaty : Window
    {
        private static AbrogateTreaty instance;
        public static AbrogateTreaty INSTANCE
        {
            get
            {
                if (instance == null || !instance.IsLoaded)
                {
                    instance = new AbrogateTreaty();
                }
                return instance;
            }
        }
        public AbrogateTreaty()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
