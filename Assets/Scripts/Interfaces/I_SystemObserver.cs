using System;

public interface I_SystemObserver
{
    void onEvent(Tuple<string , object> eventData);
}
