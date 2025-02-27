using System;
using UnityEngine;

public abstract class A_Enemy : MonoBehaviour
{
    public Action<GameObject, float> enemyTouchedByProjectile { get; set; }

    public float CurrentHealth { get; set; }
    protected int Speed { get; set; }

    public int Point { get; set; }

    public bool IsMoving { get; set; }
    public I_MoveStrategy MoveStrategy { get; set; }
}
