using System;
using UnityEngine;

public class NoMoney : I_Event
{
    private I_SystemObserver systemObserver;

    public NoMoney(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        Debug.Log("No Money");
        systemObserver.onEvent(new Tuple<string, object>("SelectingOnTowerShop", null));
        systemObserver.onEvent(new Tuple<string, object>("Notification", "Tu es pauvre!"));
        return null;
    }
}
