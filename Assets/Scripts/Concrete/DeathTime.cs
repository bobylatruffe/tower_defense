using System;
using UnityEngine;
using UnityEditor;

public class DeathTime : I_State
{
    private I_SystemObserver systemObserver;
    private int currentLevel;

    public DeathTime(I_SystemObserver systemObserver, int currentLevel)
    {
        this.systemObserver = systemObserver;
        this.currentLevel = currentLevel;
    }

    public void start()
    {
        systemObserver.onEvent(new Tuple<string, object>("PLAYER_IS_DEATH", currentLevel));
    }

    public void end()
    {
    }
}
