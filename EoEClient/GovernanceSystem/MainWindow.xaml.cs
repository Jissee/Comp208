﻿using EoE.Client.WarSystem;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance;
        public static MainWindow INSTANCE
        {
            get
            {
                if (instance == null || !instance.IsLoaded)
                {
                    instance = new MainWindow();
                }
                return instance;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Manage_Resource_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<ResourceManage>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MilitaryManagement>();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<BlockManagement>();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SetExploreWindow>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            instance = null;
        }
    }
}