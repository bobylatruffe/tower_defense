public class GameManager : I_UIObserver, I_GameManagerMediator
{
    private A_GamboardManager gamboardManager;
    private A_WaveManager waveManager;
    private A_ShopManager shopManager;
    private A_PlayerManager playerManager;
    private I_SystemObserver systemObserver;

    public void start()
    {

    }

    public void end()
    {

    }

    public void onEventFromUI(object data)
    {
        throw new System.NotImplementedException();
    }

    public void onEventFromManagers(object data)
    {
        throw new System.NotImplementedException();
    }
}
