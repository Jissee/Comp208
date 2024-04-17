using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.WarSystem
{
    internal class ClientWarDeclarableList : IClientWarDeclarableList
    {
        public List<string> DeclarableNames { get; set; }
        public ClientWarDeclarableList() 
        {
            DeclarableNames = new List<string>();
        }
        public void ChangeWarDeclarable(string[] name)
        {
            DeclarableNames.Clear();
            DeclarableNames.AddRange(name);

            Application.Current.Dispatcher.Invoke(() =>
            {
                DeclareWar window = WindowManager.INSTANCE.GetWindows<DeclareWar>();
                ListBox listBox = window.listBox1;
                listBox.Items.Clear();
                foreach (string theName in name)
                {
                    listBox.Items.Add(theName);
                }
            });
        }
    }
}
