using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarProtectors
    {
        List<string> ProtectorsName {  get; set; }
        public ClientWarProtectors(string[] names)
        {
            ProtectorsName = [.. names];
        }
    }
}
