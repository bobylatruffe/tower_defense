using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    public PossibleUpgrade possibleUpgrade { get; set; }

    protected I_TowerStrategy Strategy { get; set; }

    public abstract void shoot();
}