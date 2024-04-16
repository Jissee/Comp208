using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarNameList
    {
        public Dictionary<string, string> WarNameList {  get; set; }
        public ClientWarNameList() 
        {
            WarNameList = new Dictionary<string, string>();
        }
        public void ChangeWarName(string target, string warName)
        {
            if (WarNameList.ContainsKey(target))
            {
                WarNameList[target] = warName;
            }
            else 
            {
                WarNameList.Add(target, warName);
            }
        }
    }
}
