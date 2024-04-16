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

namespace EoE.Client.ChatSystem
{
    /// <summary>
    /// EstablishRelation.xaml 的交互逻辑
    /// </summary>
    public partial class EstablishRelation : Window
    {
        private static EstablishRelation instance;
        public static EstablishRelation INSTANCE
        {
            get
            {
                if (instance == null || !instance.IsLoaded)
                {
                    instance = new EstablishRelation();
                }
                return instance;
            }
        }
        public EstablishRelation()
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
            Res1.IsReadOnly = true;
            Res2.IsReadOnly = true;
            Res3.IsReadOnly = true;
            Res4.IsReadOnly = true;
            Res5.IsReadOnly = true;
            Res6.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)_protected.IsChecked || (bool)common.IsChecked||(bool)protecting.IsChecked)
            {
                 MessageBox.Show("You have already sent the request.");
            }
            else
            {
                MessageBox.Show("Please select a treaty!");
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
            TreatyContent treatyContent = new TreatyContent();
            treatyContent.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Res1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ResourceManage resourceManage = new ResourceManage();
            resourceManage.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}
