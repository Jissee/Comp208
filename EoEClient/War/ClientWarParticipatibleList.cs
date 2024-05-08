using EoE.War.Interface;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.War
{
    public class ClientWarParticipatibleList : IClientWarParticipatibleList
    {
        public Dictionary<string, ClientWarParticipatible> WarParticipatibleList { get; set; }
        public ClientWarParticipatibleList()
        {
            WarParticipatibleList = new Dictionary<string, ClientWarParticipatible>();
        }
        public void ChangeWarPaticipatorsList(string name, string[] participators)
        {
            ClientWarParticipatible warParticipatible = new ClientWarParticipatible(participators);
            if (WarParticipatibleList.ContainsKey(name))
            {
                WarParticipatibleList[name] = warParticipatible;
            }
            else
            {
                WarParticipatibleList.Add(name, warParticipatible);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                DeclareWar window = WindowManager.INSTANCE.GetWindows<DeclareWar>();
                ListBox listBox = window.listBox2;
                listBox.Items.Clear();
                foreach (string participator in participators)
                {
                    listBox.Items.Add(participator);
                }
            });
        }
    }
}
