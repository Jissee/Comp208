using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
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
            //todo show things to windows
        }
    }
}
