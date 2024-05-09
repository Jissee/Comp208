namespace EoE
{
    /// <summary>
    /// Managing the client windows.
    /// </summary>
    public interface IWindowManager
    {

        void ShowGameSettingWindow();
        void UpdateGameSetting(int playerNumber, int gameRound);
        void ShowGameEntterWindow();
        void ShowGameMainPage();
    }
}
