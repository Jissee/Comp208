using EoE.Client.Login;
using EoE.Governance;
using EoE.Trade;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace EoE.Client.Trade
{
    /// <summary>
    /// SelectTraderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTraderWindow : Window
    {
        public SelectTraderWindow()
        {
            InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.INSTANCE.ShowWindows<MainTradeWindow>();
            this.Hide();

        }

        private void submit_click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(sellSilicon.Text, out int seSilicon) && seSilicon >= 0
                && int.TryParse(sellCopper.Text, out int seCopper) && seCopper >= 0
                && int.TryParse(sellIron.Text, out int seIron) && seIron >= 0
                 && int.TryParse(sellAluminum.Text, out int seAluminum) && seAluminum >= 0
                  && int.TryParse(sellElectronic.Text, out int seElectronic) && seElectronic >= 0
                   && int.TryParse(sellIndustrial.Text, out int seIndustrial) && seIndustrial >= 0
                )
            {
                if (int.TryParse(buySilicon.Text, out int bySilicon) && bySilicon >= 0
                && int.TryParse(buyCopper.Text, out int byCopper) && byCopper >= 0
                && int.TryParse(buyIron.Text, out int byIron) && byIron >= 0
                 && int.TryParse(buyAluminum.Text, out int byAluminum) && byAluminum >= 0
                  && int.TryParse(buyElectronic.Text, out int byElectronic) && byElectronic >= 0
                   && int.TryParse(buyIndustrial.Text, out int byIndustrial) && byIndustrial >= 0
                )
                {
                    if (players.SelectedItem != null)
                    {
                        Guid id = Guid.NewGuid();
                        List<ResourceStack> offerorOffer =
                        [
                            new ResourceStack(GameResourceType.Silicon, seSilicon),
                            new ResourceStack(GameResourceType.Copper, seCopper),
                            new ResourceStack(GameResourceType.Iron, seIron),
                            new ResourceStack(GameResourceType.Aluminum, seAluminum),
                            new ResourceStack(GameResourceType.Electronic, seElectronic),
                            new ResourceStack(GameResourceType.Industrial, seIndustrial),
                        ];

                        List<ResourceStack> recrientOffer =
                        [
                            new ResourceStack(GameResourceType.Silicon, bySilicon),
                            new ResourceStack(GameResourceType.Copper, byCopper),
                            new ResourceStack(GameResourceType.Iron, byIron),
                            new ResourceStack(GameResourceType.Aluminum, byAluminum),
                            new ResourceStack(GameResourceType.Electronic, byElectronic),
                            new ResourceStack(GameResourceType.Industrial, byIndustrial),
                        ];

                        string recrientName = players.SelectedItem.ToString();
                        bool exist = false;
                        foreach (string playerName in Client.INSTANCE.OtherPlayer)
                        {
                            if (recrientName == playerName)
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (exist)
                        {
                            GameTransaction transaction = new GameTransaction(Client.INSTANCE.PlayerName, id, offerorOffer, recrientOffer, false, recrientName);
                            Client.INSTANCE.TradeManager.CreateSecretTransaction(transaction);
                        }
                        else
                        {
                            throw new Exception("No such name");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please select a player");
                    }
                }
                else
                {
                    MessageBox.Show("Please input a positive value.");
                }
            }
            else
            {
                MessageBox.Show("Please input a positive value.");
            }
        }

        public void SynchronizeOtherPlayerList()
        {
            players.Items.Clear();
            foreach (string name in Client.INSTANCE.OtherPlayer)
            {
                players.Items.Add(name);
            }
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
