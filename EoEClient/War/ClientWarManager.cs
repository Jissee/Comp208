using EoE.War.Interface;

namespace EoE.Client.War
{
    public class ClientWarManager : IClientWarManager
    {
        public IClientWarDeclarableList ClientWarDeclarableList { get; set; }
        public IClientWarInformationList ClientWarInformationList { get; set; }
        public IClientWarNameList ClientWarNameList { get; set; }
        public IClientWarNameRelatedList ClientWarNameRelatedList { get; set; }
        public IClientWarParticipatibleList ClientWarParticipatibleList { get; set; }
        public IClientWarProtectorsList ClientWarProtectorsList { get; set; }
        public IClientWarTargetList ClientWarTargetList { get; set; }
        public IClientWarWidthList ClientWarWidthList { get; set; }
        public ClientWarManager()
        {
            ClientWarDeclarableList = new ClientWarDeclarableList();
            ClientWarInformationList = new ClientWarInformationList();
            ClientWarProtectorsList = new ClientWarProtectorsList();
            ClientWarParticipatibleList = new ClientWarParticipatibleList();
            ClientWarTargetList = new ClientWarTargetList();
            ClientWarNameList = new ClientWarNameList();
            ClientWarWidthList = new ClientWarWidthList();
            ClientWarNameRelatedList = new ClientWarNameRelatedList();
        }

    }
}
