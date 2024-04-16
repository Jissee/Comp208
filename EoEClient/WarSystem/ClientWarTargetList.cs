using EoE.Server.WarSystem;
using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarTargetList : IClientWarTargetList
    {
        public Dictionary<string, WarTarget> WarTargetList { get; set; }
        public ClientWarTargetList() 
        {
            WarTargetList = new Dictionary<string, WarTarget>();
        }
        public void ChangeClaim(string name, WarTarget warTarget)
        {
            if (WarTargetList.ContainsKey(name))
            {
                WarTargetList[name] = warTarget;
            }
            else
            {
                WarTargetList.Add(name, warTarget);
            }
            //todo show things to windows
        }
    }
}
