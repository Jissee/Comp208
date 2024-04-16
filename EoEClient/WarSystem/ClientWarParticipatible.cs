using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarParticipatible
    {
        public List<string> Participators { get; set; }
        public ClientWarParticipatible(string[] names)
        {
            Participators = [..names];
        }
    }
}
