using System;

public class UpdateMoney : I_Event
{
    private I_SystemObserver systemObserver;

    public UpdateMoney(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", eventData));
    }
}
