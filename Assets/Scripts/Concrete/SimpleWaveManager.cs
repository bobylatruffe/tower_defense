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
        // typeof(SimpleWalkingToDestinationHighSpeedStrategy),
        // typeof(SimpleWalkingRodingStrategy)
    };

    private List<Type> flyingMoveStrategies = new List<Type>
    {
        // typeof(SimpleFlyingStrategy),
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mediator = GameManager.Instance;
        EnemyAbstractFactory = FindFirstObjectByType<SimpleAEnemieFactory>();
    }

    public override void startWave()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnWalkingEnemy();
            // StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        float elapsedTime = 0f;
        float spawnDuration = 10f;

        while (elapsedTime < spawnDuration)
        {
            switch (Random.Range(0, 1))
            {
                case 0:
                    SpawnWalkingEnemy();
                    // SpawnFlyingEnemy();
                    break;
                case 1:
                    // SpawnWalkingEnemy();
                    // SpawnTeleportingEnemy();
                    // SpawnFlyingEnemy();
                    break;
                case 2:
                    // SpawnWalkingEnemy();
                    // SpawnFlyingEnemy();
                    break;
            }

            float waitTime = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(waitTime);

            elapsedTime += waitTime;
        }
    }

    private void SpawnWalkingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createWalkingEnemie();

        Type randomStrategyType = walkingMoveStrategies[Random.Range(0, walkingMoveStrategies.Count)];
        I_MoveStrategy strategy = (I_MoveStrategy)enemy.gameObject.AddComponent(randomStrategyType);

        strategy.setDestination(Mediator.onEventFromManagers(new Tuple<string, object>("GET_LEAVE", null)));
        enemy.MoveStrategy = strategy;
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnTeleportingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createTeleportingEnemie();
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnFlyingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createFlyingEnemie();

        Type randomStrategyType = flyingMoveStrategies[Random.Range(0, flyingMoveStrategies.Count)];
        I_MoveStrategy strategy = (I_MoveStrategy)enemy.gameObject.AddComponent(randomStrategyType);

        strategy.setDestination(Mediator.onEventFromManagers(new Tuple<string, object>("GET_LEAVE", null)));
        enemy.MoveStrategy = strategy;

        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }
}