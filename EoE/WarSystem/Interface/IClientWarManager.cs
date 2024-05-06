using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.WarSystem.Interface
{
    public interface IClientWarManager
    {
        IClientWarDeclarableList ClientWarDeclarableList { get; }
        IClientWarInformationList ClientWarInformationList { get; }
        IClientWarNameList ClientWarNameList { get; }
        IClientWarNameRelatedList ClientWarNameRelatedList { get; }
        IClientWarParticipatibleList ClientWarParticipatibleList { get; }
        IClientWarProtectorsList ClientWarProtectorsList { get; }
        IClientWarTargetList ClientWarTargetList { get; }
        IClientWarWidthList ClientWarWidthList { get; }
    }
}
