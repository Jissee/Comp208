using EoE.WarSystem.Interface;
using System.Text;
using System.Windows;

namespace EoE.Client.WarSystem
{
    public class ClientWarProtectorsList : IClientWarProtectorsList
    {
        Dictionary<string, ClientWarProtectors> WarProtectorsList { get; set; }
        public ClientWarProtectorsList()
        {
            WarProtectorsList = new Dictionary<string, ClientWarProtectors>();
        }
        public void ChangeWarProtectorsList(string name, string[] protectors)
        {
            ClientWarProtectors warProtectors = new ClientWarProtectors(protectors);
            if (WarProtectorsList.ContainsKey(name))
            {
                WarProtectorsList[name] = warProtectors;
            }
            else
            {
                WarProtectorsList.Add(name, warProtectors);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                DeclareWar window = WindowManager.INSTANCE.GetWindows<DeclareWar>();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string protector in protectors)
                {
                    stringBuilder.Append($" \"{protector}\" ");
                }
                window.aliance.Text = stringBuilder.ToString();
            });
        }
    }
}
