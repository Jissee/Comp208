using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
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
            //todo show things to windows
        }
    }
}
