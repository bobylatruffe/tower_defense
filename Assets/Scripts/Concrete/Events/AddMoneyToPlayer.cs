using System;

public class AddMoneyToPlayer : I_Event
{
    private A_Player player;
    private I_SystemObserver systemObserver;

    public AddMoneyToPlayer(A_Player player, I_SystemObserver systemObserver)
    {
        this.player = player;
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        int money = player.addMoney((int)eventData);
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", money));
    }
}
