using EoE.Client.WarSystem;
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
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<CheckOtherFieldsWindow>();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<ConvertPage>();
        }

    }
}