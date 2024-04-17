using EoE.Network.Packets.GonverancePacket.Record;
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

namespace EoE.Client.GovernanceSystem
{
    /// <summary>
    /// CheckOtherPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class CheckOtherPlayer : Window
    {
        public CheckOtherPlayer()
        {
            InitializeComponent();
            InitialOtherPlayersFields();
        }
        public void InitialOtherPlayersFields()
        {
            TextBlock palyerNmae = new TextBlock
            {
                Text = "Player name",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(palyerNmae, 0);
            Grid.SetColumn(palyerNmae,0);
            OtherPlayerFields.Children.Add(palyerNmae);

            TextBlock Silicon = new TextBlock
            {
                Text = "Silicon fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Silicon, 0);
            Grid.SetColumn(Silicon, 1);
            OtherPlayerFields.Children.Add(Silicon);

            TextBlock Copper = new TextBlock
            {
                Text = "Copper fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Copper, 0);
            Grid.SetColumn(Copper, 2);
            OtherPlayerFields.Children.Add(Copper);

            TextBlock Iron = new TextBlock
            {
                Text = "Iron fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Iron, 0);
            Grid.SetColumn(Iron, 3);
            OtherPlayerFields.Children.Add(Iron);

            TextBlock Aluminum = new TextBlock
            {
                Text = "Aluminum fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Aluminum, 0);
            Grid.SetColumn(Aluminum, 4);
            OtherPlayerFields.Children.Add(Aluminum);

            TextBlock Electronic = new TextBlock
            {
                Text = "Electronic fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Electronic, 0);
            Grid.SetColumn(Electronic, 5);
            OtherPlayerFields.Children.Add(Electronic);

            TextBlock Industrial = new TextBlock
            {
                Text = "Industrial fields",
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(Industrial, 0);
            Grid.SetColumn(Industrial, 6);
            OtherPlayerFields.Children.Add(Industrial);
        }

        public void SynchronizeOtherPlayersFields()
        {
            OtherPlayerFields.Children.Clear();
            InitialOtherPlayersFields();
            List<int> fields = Client.INSTANCE.GonveranceManager.FieldList.ToList();

            TextBlock palyerNmae = new TextBlock
            {
                Text = Client.INSTANCE.PlayerName,
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(palyerNmae, 1);
            Grid.SetColumn(palyerNmae, 0);
            OtherPlayerFields.Children.Add(palyerNmae);

            for (int i = 0; i < 6; i++)
            {
                TextBlock temp = new TextBlock
                {
                    Text = fields[i].ToString(),
                    Padding = new Thickness(5),
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetRow(temp, 1);
                Grid.SetColumn(temp, i+1);
                OtherPlayerFields.Children.Add(temp);
            }

            int row = 1;
            foreach (string name in Client.INSTANCE.OtherPlayerFields.Keys)
            {
                List<FieldListRecord> otherFields = Client.INSTANCE.OtherPlayerFields[name];
                if (name != Client.INSTANCE.PlayerName)
                {
                    row++;
                    TextBlock otherpalyerNmae = new TextBlock
                    {
                        Text = name,
                        Padding = new Thickness(5),
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };
                    Grid.SetRow(otherpalyerNmae, row);
                    Grid.SetColumn(otherpalyerNmae, 0);
                    OtherPlayerFields.Children.Add(otherpalyerNmae);


                    for (int i = 0; i < 6; i++)
                    {
                        TextBlock temp2 = new TextBlock
                        {
                            Text = otherFields[i].ToString(),
                            Padding = new Thickness(5),
                            Margin = new Thickness(5),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center
                        };
                        Grid.SetRow(temp2, row);
                        Grid.SetColumn(temp2, i + 1);
                        OtherPlayerFields.Children.Add(temp2);
                    }
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
