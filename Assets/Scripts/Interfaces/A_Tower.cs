using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    protected I_GameManagerMediator Mediator { get; set; }

    public PossibleUpgrade possibleUpgrade { get; set; }

    protected I_TowerStrategy Strategy { get; set; }

    public abstract void shoot();

    protected virtual void Start()
    {
        Mediator = GameManager.Instance;
    }
}
