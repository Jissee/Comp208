using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //todo show things to windows
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
