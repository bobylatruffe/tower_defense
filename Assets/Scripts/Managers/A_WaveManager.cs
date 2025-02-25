using System;
using System.Collections;
using UnityEngine;

public abstract class A_WaveManager : MonoBehaviour
{
    public static A_WaveManager Instance { get; protected set; }

    protected I_GameManagerMediator Mediator { get; set; }
    protected I_EnemieAbstractFactory EnemyAbstractFactory { get; set; }

    protected int CurrentLevel { get; set; }

    public abstract void startWave(Action callback);
}
