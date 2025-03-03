using System;

public class SelectingOnTowerShop : I_Event
{
    private I_SystemObserver systemObserver;

    public SelectingOnTowerShop(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        systemObserver.onEvent(new Tuple<string, object>("SelectingOnTowerShop", eventData));
        return null;
    }
}
