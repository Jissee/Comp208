﻿ using System;
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
    /// CheckStatus.xaml 的交互逻辑
    /// </summary>
    public partial class CheckStatus : Window
    {
        public CheckStatus()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<CheckWarDetail>();
        }

<<<<<<< HEAD
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
=======
        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

>>>>>>> update some packets and interfaces
        }
    }
}
