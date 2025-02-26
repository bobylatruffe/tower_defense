using System;

public class UpdateTimerBeforeWave : I_Event
{
    private I_SystemObserver systemObserver;

    public UpdateTimerBeforeWave(I_SystemObserver systemObserver)
    {
        this.systemObserver = systemObserver;
    }

    public object execute(object eventData)
    {
        return systemObserver.onEvent(new Tuple<string, object>("UPDATE_TIMER_BEFORE_WAVE", eventData));
    }
}
