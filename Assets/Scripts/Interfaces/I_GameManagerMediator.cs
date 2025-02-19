using System;
using UnityEngine;

public abstract class I_GameManagerMediator : MonoBehaviour
{
    public abstract GameObject onEventFromManagers(Tuple<string, object> eventData);
}
