using System;

public interface I_UIObserver
{
    void onEventFromUI(Tuple<string, object> dataEvent);
}