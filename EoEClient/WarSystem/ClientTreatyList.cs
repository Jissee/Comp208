using EoE.WarSystem.Interface;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.WarSystem
{
    public class ClientTreatyList : IClientTreatyList
    {
        public List<string> names;
        public ClientTreatyList()
        {
            names = new List<string>();
        }
        public void AddTreatyList(string signName)
        {
            if (!names.Contains(signName))
            {
                names.Add(signName);
            }
        }

        public void ChangeTreatyList(string[] signName)
        {
            for (int i = 0; i < signName.Length; i++)
            {
                if (!names.Contains(signName[i]))
                {
                    names.Add(signName[i]);
                }
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                AbrogateTreaty window = WindowManager.INSTANCE.GetWindows<AbrogateTreaty>();
                ListBox listBox = window.PlayerList;
                listBox.Items.Clear();
                foreach (string name in signName)
                {
                    listBox.Items.Add(name);
                }
            });
        }

        public void RemoveTreatyList(string signName)
        {
            if (names.Contains(signName))
            {
                names.Remove(signName);
            }
        }
    }
}
