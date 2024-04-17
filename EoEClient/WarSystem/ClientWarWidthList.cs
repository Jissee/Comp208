using EoE.WarSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.Client.WarSystem
{
    public class ClientWarWidthList : IClientWarWidthList
    {
        public Dictionary<string, int> WarWidthList {  get; set; }
        public ClientWarWidthList()
        {
            WarWidthList = new Dictionary<string, int>();
        }
        public void ChangeWarWidth(string warName, int width)
        {
            if (WarWidthList.ContainsKey(warName))
            {
                WarWidthList[warName] = width;
            }
            else
            {
                WarWidthList.Add(warName, width);
            }
            // todo show
        }
    }
}
