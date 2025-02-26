using System;

public class ShowMainMenu : I_Event
{
    private I_SystemObserver systemObserver;

    public ShowMainMenu(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("SHOW_MAIN_MENU", eventData));
    }
}
