using System;

public class RemoveMoney : I_Event
{
    private I_SystemObserver systemObserver;
    private A_PlayerManager player;

    public RemoveMoney(I_SystemObserver systemObserver, A_PlayerManager player)
    {
        this.systemObserver = systemObserver;
        this.player = player;
    }

    public object execute(object eventData)
    {
        int newMoney = player.removeMoney((int)eventData);
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", newMoney));
    }
}
