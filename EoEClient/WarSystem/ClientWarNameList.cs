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
    public class ClientWarNameList : IClientWarNameList
    {
        public List<string> WarNameList {  get; set; }
        public ClientWarNameList() 
        {
            WarNameList = new List<string>();
        }
        public void ChangeWarName(string warName)
        {
            if (!WarNameList.Contains(warName))
            {
                WarNameList.Add(warName);
            }
        }
        public void ChangeWarNames(string[] warNames)
        {
            for(int i = 0; i < warNames.Length; i++)
            {
                ChangeWarName(warNames[i]);
            }


            Application.Current.Dispatcher.Invoke(() => {
                if (WarNameList.Contains(WarMainPage.theWarName))
                {
                    MessageBox.Show("War name exists!");
                }
                else
                {
                    DeclareWar declareWar = new DeclareWar();
                    WindowManager.INSTANCE.ShowWindows<DeclareWar>();
                    DeclareWar window = WindowManager.INSTANCE.GetWindows<DeclareWar>();
                    window.listBox2.Items.Clear();
                }
            });
        }
    }
}
