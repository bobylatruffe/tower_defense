using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    private GameObject ProjectileSpawn { get; set; }
    public I_TowerStrategy Strategy { get; set; }

    public abstract void shoot();
}