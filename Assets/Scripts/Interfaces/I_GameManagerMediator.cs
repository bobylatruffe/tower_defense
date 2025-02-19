using System;
using UnityEngine;

public interface I_GameManagerMediator
{
    GameObject onEventFromManagers(Tuple<string, object> eventData);
}