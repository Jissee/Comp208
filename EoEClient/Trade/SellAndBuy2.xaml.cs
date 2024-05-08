using EoE.Client.Login;
using EoE.Governance;
using EoE.Trade;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace EoE.Client.Trade
{
    /// <summary>
    /// SellAndBuy2.xaml 的交互逻辑
    /// </summary>
    public partial class SellAndBuy2 : Window
    {

        int[] sellValues = new int[6];
        int[] buyValues = new int[6];
        public SellAndBuy2()
        {
            InitializeComponent();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sellSilicon.Text, out int sellerSilicon) && (sellerSilicon >= 0) &&
                int.TryParse(sellCopper.Text, out int sellerCopper) && (sellerCopper >= 0) &&
                int.TryParse(sellIron.Text, out int sellerIron) && (sellerIron >= 0) &&
                int.TryParse(sellAlluminum.Text, out int sellerAlluminum) && (sellerAlluminum >= 0) &&
                int.TryParse(sellElecronic.Text, out int sellerElecronic) && (sellerElecronic >= 0) &&
                int.TryParse(sellIndustrial.Text, out int sellerIndustrial) && (sellerIndustrial >= 0) &&
                int.TryParse(buySilicon.Text, out int buyerSilicon) && (buyerSilicon >= 0) &&
                int.TryParse(buyCopper.Text, out int buyerCopper) && (buyerCopper >= 0) &&
                int.TryParse(buyIron.Text, out int buyerIron) && (buyerIron >= 0) &&
                int.TryParse(buyAlluminum.Text, out int buyerAlluminum) && (buyerAlluminum >= 0) &&
                int.TryParse(buyElecronic.Text, out int buyerElecronic) && (buyerElecronic >= 0) &&
                int.TryParse(buyIndustrial.Text, out int buyerIndustrial) && (buyerIndustrial >= 0)
                )
            {
                Guid id = Guid.NewGuid();
                string offer = Client.INSTANCE.PlayerName!;
                List<ResourceStack> offerorOffer = new List<ResourceStack>();
                List<ResourceStack> recipentOffer = new List<ResourceStack>();
                offerorOffer.Add(new ResourceStack(GameResourceType.Silicon, sellerSilicon));
                offerorOffer.Add(new ResourceStack(GameResourceType.Copper, sellerCopper));
                offerorOffer.Add(new ResourceStack(GameResourceType.Iron, sellerIron));
                offerorOffer.Add(new ResourceStack(GameResourceType.Aluminum, sellerAlluminum));
                offerorOffer.Add(new ResourceStack(GameResourceType.Electronic, sellerElecronic));
                offerorOffer.Add(new ResourceStack(GameResourceType.Industrial, sellerIndustrial));

                recipentOffer.Add(new ResourceStack(GameResourceType.Silicon, buyerSilicon));
                recipentOffer.Add(new ResourceStack(GameResourceType.Copper, buyerCopper));
                recipentOffer.Add(new ResourceStack(GameResourceType.Iron, buyerIron));
                recipentOffer.Add(new ResourceStack(GameResourceType.Aluminum, buyerAlluminum));
                recipentOffer.Add(new ResourceStack(GameResourceType.Electronic, buyerElecronic));
                recipentOffer.Add(new ResourceStack(GameResourceType.Industrial, buyerIndustrial));
                GameTransaction gameTransaction = new GameTransaction(offer, id, offerorOffer, recipentOffer, true, null);
                Client.INSTANCE.TradeManager.CreateOpenTransaction(gameTransaction);
            }
            else
            {
                MessageBox.Show("Please input a natural number");
            }
            this.Hide();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MainTradeWindow>();
            this.Hide();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
