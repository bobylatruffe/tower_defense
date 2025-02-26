using System;

public class UpdateLifePoint : I_Event
{
    private I_SystemObserver systemObserver;

    public UpdateLifePoint(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", eventData));
    }
}
