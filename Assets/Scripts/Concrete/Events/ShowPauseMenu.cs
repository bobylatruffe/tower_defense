using System;

public class ShowPauseMenu : I_Event
{
    private I_SystemObserver systemObserver;
    private GameManager gameManager;

    public ShowPauseMenu(GameManager gameManager, I_SystemObserver systemObserver)
    {
        this.gameManager = gameManager;
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        if (gameManager.getState().GetType() == typeof(InitTime))
            return null;

        systemObserver.onEvent(new Tuple<string, object>("ShowPauseMenu", null));
        return null;
    }
}
