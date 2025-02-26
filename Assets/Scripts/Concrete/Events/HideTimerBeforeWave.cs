using System;

public class HideTimerBeforeWave : I_Event
{
    private I_SystemObserver systemObserver;

    public HideTimerBeforeWave(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("HIDE_TIMER_BEFORE_WAVE", null));
    }
}
