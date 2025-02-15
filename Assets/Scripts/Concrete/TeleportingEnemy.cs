
using System.Collections;
using UnityEngine;

public class TeleportingEnemy : A_Enemie
{
    private void Start()
    {
        StartCoroutine(TeleportRandomly());
    }

    private IEnumerator TeleportRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 5f));

            if (Random.Range(0, 100) < 80)
            {
                transform.position += new Vector3(0f, 0f, 2f);
            }
        }
    }

    private void Update()
    {
        move();
    }
}