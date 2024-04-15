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
            ResourceManage resourceManage = new ResourceManage();
            resourceManage.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MilitaryManagement militaryManagement = new MilitaryManagement();
            militaryManagement.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BlockManagement blockManagement = new BlockManagement();
            blockManagement.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ExpolreBlock expolreBlock = new ExpolreBlock();
            expolreBlock.Show();
        }
    }
}