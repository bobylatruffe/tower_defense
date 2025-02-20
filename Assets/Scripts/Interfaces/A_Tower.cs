using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    public GameObject ProjectileSpawn { get; set; }
    protected I_TowerStrategy Strategy { get; set; }
}