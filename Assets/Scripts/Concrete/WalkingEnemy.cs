using UnityEngine;

public class WalkingEnemy : A_Enemie
{
    private void Update()
    {
        move();
    }

    protected override void move()
    {
        transform.Translate(Vector3.forward * Random.Range(1f, 3f) * Time.deltaTime);
    }
}