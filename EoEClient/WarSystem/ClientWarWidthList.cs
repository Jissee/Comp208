using EoE.WarSystem.Interface;
using System.Windows;

namespace EoE.Client.WarSystem
{
    public class ClientWarWidthList : IClientWarWidthList
    {
        public Dictionary<string, int> WarWidthList { get; set; }
        public ClientWarWidthList()
        {
            WarWidthList = new Dictionary<string, int>();
        }
        public void ChangeWarWidth(string warName, int width)
        {
            if (WarWidthList.ContainsKey(warName))
            {
                WarWidthList[warName] = width;
            }
            else
            {
                WarWidthList.Add(warName, width);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllocateArmy window = WindowManager.INSTANCE.GetWindows<AllocateArmy>();
                window.battleafter.Text = width.ToString();
            });
        }
    }
}
