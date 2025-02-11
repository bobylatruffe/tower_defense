using System;

public interface I_GameManagerMediator
{
    void onEventFromManagers(Tuple<string, object> eventData);
}
