using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Comp208WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate int IntToInt(int x);
        private event IntToInt ev;

        private static int plusOne(int x) 
        {
            return x + 1; 
        }
        private static int minusOne(int x)
        {
            return x - 1;
        }
        private static int square(int x)
        {
            return x * x;
        }
        public MainWindow()
        {
            InitializeComponent();
            

            menu1item1.Click += (sender, args) =>
            {
                MessageBox.Show(sender.ToString());
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("123");
            ev += plusOne;
            ev += square;
            IntToInt i2i = plusOne;
            i2i += minusOne;
            i2i += square;
            foreach(IntToInt intToInt in i2i.GetInvocationList())
            {
                MessageBox.Show(intToInt.GetMethodInfo().Name);
            }

            //MessageBox.Show(ev(10).ToString());
        }

        private void Rectangle_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.A)
            {
                throw new NotImplementedException("A pressed.");
            }
        }
    }
}
