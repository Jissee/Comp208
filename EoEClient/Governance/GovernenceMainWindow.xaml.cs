﻿using System.Windows;

namespace EoE.Client.Governance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GovernenceMainWindow : Window
    {

        public GovernenceMainWindow()
        {
            InitializeComponent();
        }

        private void Manage_Resource_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<ResourceInformationWindow>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MilitaryManagementWindow>();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<FieldManagementWindow>();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<SetExploreWindow>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<CheckOtherPlayer>();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<UpgradeFieldWindow>();
        }
    }
}