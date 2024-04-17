using EoE.Client.GovernanceSystem;
using EoE.Client.Login;
using EoE.GovernanceSystem;
using EoE.TradeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EoE.Client.TradeSystem
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
            if (int.TryParse(sellSilicon.Text ,out int seSilicon) && seSilicon>=0
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
                        Guid id = new Guid();
                        List<ResourceStack> offerorOffer = new List<ResourceStack>();
                        offerorOffer.Add(new ResourceStack(GameResourceType.Silicon, seSilicon));
                        offerorOffer.Add(new ResourceStack(GameResourceType.Copper, seCopper));
                        offerorOffer.Add(new ResourceStack(GameResourceType.Iron, seIron));
                        offerorOffer.Add(new ResourceStack(GameResourceType.Aluminum, seAluminum));
                        offerorOffer.Add(new ResourceStack(GameResourceType.Electronic, seElectronic));
                        offerorOffer.Add(new ResourceStack(GameResourceType.Industrial, seIndustrial));

                        List<ResourceStack> recrientOffer = new List<ResourceStack>();
                        recrientOffer.Add(new ResourceStack(GameResourceType.Silicon, bySilicon));
                        recrientOffer.Add(new ResourceStack(GameResourceType.Copper, byCopper));
                        recrientOffer.Add(new ResourceStack(GameResourceType.Iron, byIron));
                        recrientOffer.Add(new ResourceStack(GameResourceType.Aluminum, byAluminum));
                        recrientOffer.Add(new ResourceStack(GameResourceType.Electronic, byElectronic));
                        recrientOffer.Add(new ResourceStack(GameResourceType.Industrial, byIndustrial));

                        string recrientName = players.SelectedItem.ToString();
                        foreach (string playerName in Client.INSTANCE.OtherPlayer)
                        {
                            if (recrientName == playerName)
                            {
                                GameTransaction transaction = new GameTransaction(Client.INSTANCE.PlayerName, id, offerorOffer, recrientOffer, false, recrientName);
                                Client.INSTANCE.TradeManager.RequireCreateSecretTransaction(transaction);
                            }
                            else
                            {
                                throw new Exception("No such name");
                            }
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
