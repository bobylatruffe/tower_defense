using System;
using UnityEngine;

public interface I_GameManagerMediator
{
    object onEventFromManagers(Tuple<EventTypeFromManager, object> eventFromManager);
}
