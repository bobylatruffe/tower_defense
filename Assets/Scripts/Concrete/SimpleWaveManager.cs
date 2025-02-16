using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWaveManager : A_WaveManager
{
    private List<Type> walkingMoveStrategies = new List<Type>
    {
        typeof(SimpleWalkingToDestinationStrategy),
        typeof(SimpleWalkingToDestinationHighSpeedStrategy),
        // typeof(SimpleWalkingRodingStrategy)
    };

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
                    SpawnWalkingEnemy();
                    // SpawnTeleportingEnemy();
                    break;
                case 2:
                    SpawnWalkingEnemy();
                    // SpawnFlyingEnemy();
                    break;
            }

            float waitTime = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(waitTime);

            elapsedTime += waitTime;
        }
    }

    private void SpawnWalkingEnemy()
    {
        A_Enemie enemy = enemyAbstractFactory.createWalkingEnemie();

        Type randomStrategyType = walkingMoveStrategies[Random.Range(0, walkingMoveStrategies.Count)];
        I_MoveStrategy strategy = (I_MoveStrategy)enemy.gameObject.AddComponent(randomStrategyType);

        strategy.setDestination(Mediator.onEventFromManagers(new Tuple<string, object>("GET_LEAVE", null)));
        enemy.MoveStrategy = strategy;
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