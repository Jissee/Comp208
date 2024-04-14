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

namespace WarSystem
{
    /// <summary>
    /// WarMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class WarMainPage : Window
    {
       
        public WarMainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeclareWar declareWar = new DeclareWar();
            declareWar.Show();
        }

        private void buttonCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckStatus checkStatus = new CheckStatus();
            checkStatus.Show();
        }

        private void buttonGoingWar_Click(object sender, RoutedEventArgs e)
        {
            AllocateWar allocateWar = new AllocateWar();
            allocateWar.Show();
        }

        private void buttonReview_Click(object sender, RoutedEventArgs e)
        {
            ReviewDiplomatic reviewDiplomatic = new ReviewDiplomatic(); 
            reviewDiplomatic.Show();
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void warGoals_Click(object sender, RoutedEventArgs e)
        {
            WarDetail warDetail = new WarDetail();
            warDetail.Show();
        }
    }
}
