using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarProtectorsList : IClientWarProtectorsList
    {
        Dictionary<string, ClientWarProtectors> WarProtectorsList {  get; set; }
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
            //todo show things to windows
        }
    }
}
