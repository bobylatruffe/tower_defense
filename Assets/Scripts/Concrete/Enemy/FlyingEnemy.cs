using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class FlyingEnemy : A_Enemy
{
    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
        }
    }
}