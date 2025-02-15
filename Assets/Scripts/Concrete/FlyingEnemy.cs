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

        transform.Translate(Vector3.forward * Random.Range(4f, 7f) * Time.deltaTime);


        if (heightOfFlying > 0)
        {
            heightOfFlying -= Time.deltaTime;
            transform.Translate(Vector3.up * Time.deltaTime * 1f);
        }
    }
}