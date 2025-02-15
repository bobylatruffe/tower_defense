using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWaveManager : A_WaveManager
{
    public override void startWave()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;
        float spawnDuration = 10f;

        while (elapsedTime < spawnDuration)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    SpawnWalkingEnemy();
                    break;
                case 1:
                    SpawnTeleportingEnemy();
                    break;
                case 2:
                    SpawnFlyingEnemy();
                    break;
            }

            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);

            elapsedTime += waitTime;
        }
    }

    private void SpawnWalkingEnemy()
    {
        A_Enemie enemy = enemyAbstractFactory.createWalkingEnemie();
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnTeleportingEnemy()
    {
        A_Enemie enemy = enemyAbstractFactory.createTeleportingEnemie();
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnFlyingEnemy()
    {
        A_Enemie enemy = enemyAbstractFactory.createFlyingEnemie();
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }
}