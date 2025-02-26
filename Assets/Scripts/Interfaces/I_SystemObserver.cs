using System;

public interface I_SystemObserver
{
    object onEvent(Tuple<string , object> eventData);
}
