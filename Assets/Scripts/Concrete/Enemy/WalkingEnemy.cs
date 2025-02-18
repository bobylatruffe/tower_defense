using UnityEngine;

public class WalkingEnemy : A_Enemie
{
    private void Start()
    {
        Point = 10;
    }

    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
        }
    }
}