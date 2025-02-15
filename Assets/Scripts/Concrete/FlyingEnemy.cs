using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingEnemy : A_Enemie
{
    private float heightOfFlying;

    private void Start()
    {
        heightOfFlying = Random.Range(3f, 6f);
    }

    private void Update()
    {
        move();

        if (heightOfFlying > 0)
        {
            heightOfFlying -= Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime * 1f);
        }
    }
}