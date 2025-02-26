using System;

public class UpdateLevelHud : I_Event
{
    private I_SystemObserver systemObserver;

    public UpdateLevelHud(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_LEVEL_HUD", eventData));
    }
}
