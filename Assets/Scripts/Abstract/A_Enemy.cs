using System;
using UnityEngine;

public abstract class A_Enemy : MonoBehaviour
{
    protected I_GameManagerMediator Mediator { get; set; }
    protected float CurrentHealth { get; set; }
    protected float MaxHealth { get; set; }

    protected int Speed { get; set; }

    public int Point { get; set; }

    public EnemyType EnemyType { get; set; }

    public bool IsMoving { get; set; }
    public I_MoveStrategy MoveStrategy { get; set; }
}
