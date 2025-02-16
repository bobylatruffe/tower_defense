using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class FlyingEnemy : A_Enemie
{
    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
        }
    }
}