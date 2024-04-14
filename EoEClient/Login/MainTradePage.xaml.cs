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
using WpfApp1;
using WpfApp1.Login.TradeSystem.AcceptTrade;
using WpfApp1.Login.TradeSystem.MadeBuyRequirest;
using WpfApp1.Login.TradeSystem.Make_a_trade;
using WpfApp1.MadeBuyRequirest;

namespace EoE.Client.TradeSystem
{
   
    
    public partial class MainTradePage : Window
    {
        
        public MainTradePage()
        {
            InitializeComponent();
            addItem(1);
            addItem(2);
            addItem(3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectTraderWindow selectTraderWindow = new SelectTraderWindow();
            selectTraderWindow.Show();
            this.Hide();
            
        }

        //接受交易按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(tradeList.SelectedItem != null)
            {
                RequirestList requirestList = new RequirestList();
                requirestList.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a trade to accept!");
            }
            
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //List<TradeItem> selectedItems = GetSelectedItems();
           SellAndBuy2 sellAndBuy2 = new SellAndBuy2();
            sellAndBuy2.Show();
            this.Hide();

        }

        //查找求购信息按钮
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SearchTransationWindow searchTransationWindow = new SearchTransationWindow();
            searchTransationWindow.Show();
            this.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainGamePage mainGamePage = new MainGamePage();
            mainGamePage.Show();
        }

        private void tradeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number = int.Parse(tradeList.SelectedItem.ToString());

            itemShow.Text = tradeList.SelectedItem.ToString();
        }

        public void addItem(int transcationnumber)
        {
            tradeList.Items.Add(transcationnumber);
        }
        
    }
}
