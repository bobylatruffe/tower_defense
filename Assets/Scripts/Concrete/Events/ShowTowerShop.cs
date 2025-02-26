using System;

public class ShowTowerShop : I_Event
{
    private I_SystemObserver systemObserver;

    public ShowTowerShop(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
    }
}
