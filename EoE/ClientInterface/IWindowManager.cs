using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoE.ClientInterface
{
    public interface IWindowManager
    {

        void ShowGameSettingWindow();
        void UpdateGameSetting(int playerNumber,int gameRound);
        void ShowGameEntterWindow();
        void ShowGameMainPage();
    }
}
