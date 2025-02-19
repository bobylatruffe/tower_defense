using System;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    public Func<A_Tower, float, A_Enemie> GetClosestEnemyMethod { get; set; }

    public float Range { get; set; }
    public float FireRate { get; set; }
    public GameObject ProjectileSpawn { get; set; }
    protected I_TowerState towerState;

    public abstract void setState(I_TowerState state);
    public abstract void shoot();
}