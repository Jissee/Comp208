﻿using EoE.Network.Packets.GonverancePacket.Record;
using EoE.Network.Packets.WarPacket;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EoE.Client.WarSystem
{
    /// <summary>
    /// AllocateArmy.xaml 的交互逻辑
    /// </summary>
    public partial class AllocateArmy : Window
    {
        public AllocateArmy()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Submit3_Click(object sender, RoutedEventArgs e)
        {
            int resultBattle, resultInformative, resultMechanism;

            int.TryParse(battleAllocation.Text, out resultBattle);
            int.TryParse(informativeAllocation.Text, out resultInformative);
            int.TryParse(mechanismAllocation.Text, out resultMechanism);
            FillInFrontierPacket packet = new FillInFrontierPacket(
                CheckStatus.selectedWarName,
                resultBattle, resultInformative, resultMechanism
                );
            Client.INSTANCE.SendPacket( packet );
        }


        public void SynchronizeResources(ResourceListRecord record)
        {
            battlebefore.Text = record.battleArmyCount.ToString();
            infobefore.Text = record.informativeArmyCount.ToString();
            mechbefore.Text = record.mechanismArmyCount.ToString();
        }
    }
}