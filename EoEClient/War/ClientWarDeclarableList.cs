using EoE.War.Interface;
using System.Windows;

namespace EoE.Client.War
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
                window.ChangeWarDeclarableList(name);
            });
        }
    }
}
