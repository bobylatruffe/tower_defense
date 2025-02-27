using System;

public class RemoveLife : I_Event
{
    private A_Player player;
    private I_SystemObserver systemObserver;

    public RemoveLife(A_Player player, I_SystemObserver systemObserver)
    {
        this.player = player;
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        int newLifePoint = player.removeLifePoint((int)eventData);
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", newLifePoint));
    }
}
