using System;
using System.Collections;
using UnityEngine;

public abstract class A_Wave : MonoBehaviour
{
    public static A_Wave Instance { get; protected set; }

    protected I_GameManagerMediator Mediator { get; set; }
    protected I_EnemieAbstractFactory EnemyAbstractFactory { get; set; }

    public int CurrentLevel { get; set; }

    public abstract void startWave(Action callback);
}
