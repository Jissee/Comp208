﻿using System;
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

namespace GovernenceSystem
{
    /// <summary>
    /// BlockManagement.xaml 的交互逻辑
    /// </summary>
    public partial class BlockManagement : Window
    {
        public BlockManagement()
        {
            InitializeComponent();
        }
       public void getValue()
        {

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Convert convert = new Convert();
            convert.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
