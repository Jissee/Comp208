using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IClientTreatyList
    {
        void AddTreatyList(string signName);
        void ChangeTreatyList(string[] signName);
        void RemoveTreatyList(string signName);
    }
}
