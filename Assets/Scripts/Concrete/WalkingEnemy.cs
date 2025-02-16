using UnityEngine;

public class WalkingEnemy : A_Enemie
{
    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
        }
    }
}