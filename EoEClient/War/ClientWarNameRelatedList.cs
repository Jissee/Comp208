using EoE.War.Interface;
using System.Windows;
using System.Windows.Controls;

namespace EoE.Client.War
{
    public class ClientWarNameRelatedList : IClientWarNameRelatedList
    {
        public List<string> WarNameRelatedList { get; set; }
        public ClientWarNameRelatedList()
        {
            WarNameRelatedList = new List<string>();
        }
        public void ChangeWarName(string warName)
        {
            if (!WarNameRelatedList.Contains(warName))
            {
                WarNameRelatedList.Add(warName);
            }
        }
        public void ChangeWarNames(string[] warNames)
        {
            WarNameRelatedList.Clear();
            for (int i = 0; i < warNames.Length; i++)
            {
                ChangeWarName(warNames[i]);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckStatus window = WindowManager.INSTANCE.GetWindows<CheckStatus>();
                ListBox box = window.checkStatusListBoxWarName;
                box.Items.Clear();
                foreach (string names in WarNameRelatedList)
                {
                    box.Items.Add(names);
                }
            });
        }
    }
}
